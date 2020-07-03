using OrderProcessor.Business.Interfaces;
using OrderProcessor.DTO;
using OrderProcessor.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace OrderProcessor.Business.Concrete
{
    public class OrderProcessorBusiness : IOrderProcessorBusiness
    {
        /// <summary>
        /// Submit the order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order SubmitOrder(Order order)
        {
            order.OrderStatus = OrderStatus.InProgress;

            var type = typeof(IRuleHandler);
            // get the all the concrete class inherited from IRuleHandler
            var concreteTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && !p.IsInterface).ToList();
            //Loop through each orderItems
            Parallel.ForEach(order.OrderItems, (orderItem) =>
            {
                //loop through each handler inherited from IruleHandler and check if that handler need to be executed using isValidHandler method
                Parallel.ForEach(concreteTypes, (concreteType) =>
                {
                    //Get all the dependencies from constructor for the repective handler to inject it when is is dynamically instantiated below
                    var contructorParameters = concreteType.GetConstructors().SelectMany(x => x.GetParameters()).ToList();
                    var parameters = new List<object>();
                    // loop through each dependencies and create instance of each depencies and add it to the collection so that this list
                    Parallel.ForEach(contructorParameters, (item) =>
                    {
                        Type dependencies = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).FirstOrDefault(p => Type.GetType(item.ParameterType + ", OrderProcessor.Business").IsAssignableFrom(p) && !p.IsInterface);
                        if (dependencies != null)
                        {
                            parameters.Add(Activator.CreateInstance(dependencies));
                        }
                    });
                    // dynamic instance creation for the handler and if any dependencies are their then it is inject it from above defined list of object (parameters)
                    var dynamicType = contructorParameters != null && contructorParameters.Count > 0 ? Activator.CreateInstance(concreteType, parameters.ToArray()) as IRuleHandler :
                        Activator.CreateInstance(concreteType) as IRuleHandler;
                    // Check if it is valid handler and can execute the ProcessOrderItem method
                    var isValidHandler = dynamicType.IsHandle(orderItem);
                    if (isValidHandler)
                    {
                        //All the orderItem will be processed according to handler if it is valid
                        dynamicType.ProcessOrderItem(order, orderItem);
                        // Post order stepps can be done here
                    }
                });
            });
            // order is processing is completed
            order.OrderStatus = OrderStatus.Completed;
            return order;
        }
    }
}

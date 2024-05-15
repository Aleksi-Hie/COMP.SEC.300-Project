const allOrders = require('./allOrders');

function updateOrder(order, status){
    allOrders.find(element => element.id === order.id).status = status;
}

module.exports = updateOrder;
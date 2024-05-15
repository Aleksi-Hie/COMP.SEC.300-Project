'use strict';
const allOrders = require('../utils/allOrders');
/**
 * Add an order for an sandwich
 *
 * order Order place an order for a sandwich
 * returns Order
 **/
exports.addOrder = function(order) {
  return new Promise(function(resolve, reject) {
    if(allOrders.find(element => element.id === order.id) !== undefined){
      reject("Order not created");
      
    }
    
    //else order already exists
    else if(order.id !== undefined && order.sandwichId !== undefined && order.status !== undefined){
      resolve(order);
    }
    else{
      reject("Order not created");
    }
  });
}


/**
 * Find an order by its ID
 * IDs must be positive integers
 *
 * orderId Long ID of the order that needs to be fetched
 * returns Order
 **/
exports.getOrderById = function(orderId) {
  return new Promise(function(resolve, reject) {
    if(order.id === undefined){
      reject("Invalid ID supplied");
    }
    else{
      resolve(order);
    }
  });
}


/**
 * Get a list of all orders. Empty array if no orders are found.
 *
 * returns ArrayOfOrders
 **/
exports.getOrders = function() {
  return new Promise(function(resolve, reject) {
    resolve(allOrders);
  });
}


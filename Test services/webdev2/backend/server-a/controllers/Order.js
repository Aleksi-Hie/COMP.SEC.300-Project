'use strict';

var utils = require('../utils/writer.js');
var Order = require('../service/OrderService');
const allOrders = require('../utils/allOrders')
const sendTask = require('../rabbit-utils/sendTask');

const updateOrder = require('../utils/updateOrder');

const rabbitHost = process.env.RABBIT_HOST || "rabbitmq";
const rabbitUser = process.env.RABBITMQ_DEFAULT_USER || "myuser";
const rabbitPassword = process.env.RABBITMQ_DEFAULT_PASSWORD || "mypassword";
const sendQueue = "received-orders";
const receiveQueue = "handled-orders";
const address = `${rabbitUser}:${rabbitPassword}@${rabbitHost}`

module.exports.addOrder = function addOrder (req, res, next) {
  var order = req.swagger.params['order'].value;
  console.log("Received order: ", order)
  Order.addOrder(order)
  .then(function (response) {
    utils.writeJson(res, response);
    // Let's add the order to a queue
    // Notice: "rapid-runner-rabbit" is the name of the Docker Compose service
    // Using only Docker networking didn't work,
    // unless Docker's bridge network IPs, were used (172.20.0.X).
    allOrders.push(order);
    sendTask.addTask(address, sendQueue, order);
  })
  .catch(function (response) {
    utils.writeJson(res, response);
  });
};

module.exports.getOrderById = function getOrderById (req, res, next) {
  var orderId = req.swagger.params['orderId'].value;
  if (allOrders.find(element => element.id === orderId) === undefined) {
    utils.writeJson(res, "Order not found");
  }
  Order.getOrderById(orderId)
  //Tarkista kuinka serverit kommunikoi
    .then(function (response) {
      updateOrder(response, response.status);
      utils.writeJson(res, response);
    })
    .catch(function (response) {
      utils.writeJson(res, response);
    });
};

module.exports.getOrders = function getOrders (req, res, next) {
  Order.getOrders()
    .then(function (response) {
      utils.writeJson(res, response);
    })
    .catch(function (response) {
      utils.writeJson(res, response);
    });
};

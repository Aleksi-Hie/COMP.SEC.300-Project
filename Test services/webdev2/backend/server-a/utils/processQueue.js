const receiveTask = require('../rabbit-utils/receiveTask');
const util = require('util');
const getTaskAsync = util.promisify(receiveTask.getTask);
const updateOrder = require('../utils/updateOrder');

const rabbitHost = process.env.RABBIT_HOST || "rabbitmq";
const sendQueue = "received-orders";
const receiveQueue = "handled-orders";


module.exports = async function processQueue(rabbitUser, rabbitPassword) {
    const address = `${rabbitUser}:${rabbitPassword}@${rabbitHost}`;
    let order = await getTaskAsync(address, receiveQueue);
    if (order === null) {
      console.log("Queue is empty");
      setTimeout(processQueue, 5000); 
      return;
    }
    console.log("Received order: ", order);
    order = JSON.parse(order);
    updateOrder(order, order.status);
    await processQueue(); 
  }
  

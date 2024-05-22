# COFFEE STORE MANAGEMENT

## Overview

Coffee Shop Management System is a web-based backend application using ASP.NET Core Web API for creating RESTful services. It aims to provide functionalities for managing customers, employees, products, and orders in a coffee store. The application utilizes Entity Framework Core for data management, a Service layer for business logic, and incorporates JSON Web Tokens (JWT) for authentication. API documentation can be created through Swagger and Postman.

## Expected Features

## Expected Features

### Customer Management

- **Register:** Customers can register by providing necessary details.
- **Login:** Customers can log in to their accounts securely.
- **Place Order:** Customers can place orders for products available in the store.
- **Cancel Order:** Customers can cancel their orders based on the order status.

### Employee Management

#### Admin

- **Approve New Employee:** Admin can approve new employee registrations.
- **Register:** Admin can register new employees.
- **Login:** Admin can log in to manage the coffee store.

#### Manager

- **Register:** Managers can register themselves.
- **Login:** Managers can log in to manage the coffee store.
- **Update Status of Products:** Managers can update the status of products.

#### Barista

- **Register:** Baristas can register themselves.
- **Login:** Baristas can log in to manage their tasks.
- **Update Product Status:** Baristas can update the status of products.
- **Update Product Stock:** Baristas can update the stock of products.
- **Update Order Status:** Baristas can update the status of orders.

### Product Management

- **Get All Products:** Users can view a list of all products available in the store.
- **Get Product Based on Category:** Users can filter products based on categories.

### Order Management

- **Get All Active Orders:** Employees can view a list of all active orders in the system.
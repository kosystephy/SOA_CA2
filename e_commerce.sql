drop database if exists e_commerce;	

CREATE DATABASE e_commerce;
USE e_commerce;

CREATE TABLE Categories (
    Category_Id INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(100) NOT NULL
);

CREATE TABLE Products (
    Product_Id INT AUTO_INCREMENT PRIMARY KEY,
    Category_Id INT NOT NULL,
    Product_Name VARCHAR(100) NOT NULL,
    Brand VARCHAR(100),
    Gender ENUM('Male', 'Female', 'Unisex'),  -- For products like clothing
    Stock INT NOT NULL,
    Year INT NOT NULL,  -- Manufacturing or release year
    Description TEXT,
    Image VARCHAR(255),  -- URL or file path for product image
    FOREIGN KEY (Category_Id) REFERENCES Categories(Category_Id)
        ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE Customers (
    Customer_Id INT AUTO_INCREMENT PRIMARY KEY,
    First_Name VARCHAR(50) NOT NULL,
    Last_Name VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Salt VARCHAR(255) NOT NULL,  -- For password security
    Role ENUM('Admin', 'Customer') DEFAULT 'Customer',
    Address TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);


CREATE TABLE Orders (
    Order_Id INT AUTO_INCREMENT PRIMARY KEY,
    Customer_Id INT NOT NULL,
    Order_Date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Total_Amount DECIMAL(10, 2) NOT NULL,
    Status ENUM('Pending', 'Completed', 'Cancelled') NOT NULL,
    FOREIGN KEY (Customer_Id) REFERENCES Customers(Customer_Id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE OrderItems (
    Item_Id INT AUTO_INCREMENT PRIMARY KEY,
    Order_Id INT NOT NULL,
    Product_Id INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,  -- Price at the time of order
    FOREIGN KEY (Order_Id) REFERENCES Orders(Order_Id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Product_Id) REFERENCES Products(Product_Id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

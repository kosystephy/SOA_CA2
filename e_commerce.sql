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




-- Category_Id: 1 (Clothing)
INSERT INTO Products (Category_Id, Product_Name, Description, Price, Stock, Gender, ImageUrl)
VALUES 
(1, 'Mens Casual Shirt-classiic Fit', 'A comfortable casual shirt for daily wear.', 29.99, 100, 'Male', 'https://m.media-amazon.com/images/I/71QKYagCZ5L._AC_SY879_.jpg'),
(1, 'Womens Summer Dress -Floral', 'A lightweight dress perfect for summer.', 39.99, 50, 'Female', 'https://m.media-amazon.com/images/I/81kKMDpwX7L._AC_SX679_.jpg'),
(1, 'Unisex Hoodie -Sylish ', 'A stylish hoodie for men and women.', 49.99, 200, 'Unisex', 'https://m.media-amazon.com/images/I/5169pVJz-6L._AC_SY879_.jpg'),
(1, 'Mens Formal Trousers -Slim Fit', 'Elegant trousers for office wear.', 59.99, 30, 'Male', 'https://m.media-amazon.com/images/I/61dJm47BiPL._AC_SY879_.jpg'),
(1, 'Womens Blouse -fashionmable', 'A fashionable blouse for work or casual wear.', 24.99, 70, 'Female', 'https://m.media-amazon.com/images/I/81-9GxyD52L._AC_SY879_.jpg');

-- Category_Id: 2 (Shoes)
INSERT INTO Products (Category_Id, Product_Name, Description, Price, Stock, Gender, ImageUrl)
VALUES
(2, 'Running Shoes-LightWeight', 'Lightweight and durable running shoes.', 79.99, 120, 'Unisex', 'https://static.nike.com/a/images/t_PDP_1728_v1/f_auto,q_auto:eco/b72f84de-c4c8-4a93-bf21-ebc63c249384/W+NIKE+METCON+9+AMP.png'),
(2, 'Mens Formal Shoes- Oxford', 'Stylish formal shoes for men.', 89.99, 60, 'Male', 'https://m.media-amazon.com/images/I/71xmku-k4bL._AC_SY695_.jpg'),
(2, 'Womens Heels -Elegant', 'Elegant heels for evening wear.', 69.99, 40, 'Female', 'https://m.media-amazon.com/images/I/51nu+mEZqcL._AC_SX695_.jpg'),
(2, 'Sneakers -Trendy', 'Trendy sneakers for daily use.', 49.99, 150, 'Unisex', 'https://static.nike.com/a/images/t_PDP_1728_v1/f_auto,q_auto:eco/680dd8b4-0fb8-42a9-855b-174e8e96b1c2/NIKE+SHOX+TL.png'),
(2, 'Womens Sandals-Summer Edition', 'Comfortable sandals for summer.', 34.99, 80, 'Female', 'https://m.media-amazon.com/images/I/61cA2WpKDqL._AC_SY695_.jpg');

-- Category_Id: 3 (Accessories)
INSERT INTO Products (Category_Id, Product_Name, Description, Price, Stock, Gender, ImageUrl)
VALUES
(3, 'Leather Wallet -Premium', 'A premium leather wallet.', 19.99, 200, 'Unisex', 'https://m.media-amazon.com/images/I/91vfcmXEQLL._AC_SX679_.jpg'),
(3, 'Silk Scarf -Patterned', 'A soft silk scarf for all occasions.', 29.99, 100, 'Female', 'https://m.media-amazon.com/images/I/51mb8fNFnmL._AC_SX679_.jpg'),
(3, 'Sunglasses -UV Protection', 'Trendy sunglasses with UV protection.', 24.99, 150, 'Unisex', 'https://m.media-amazon.com/images/I/41JlRunoMLL._AC_SX679_.jpg'),
(3, 'Mens Watch -Classic', 'A classic watch with a leather strap.', 99.99, 50, 'Male', 'https://m.media-amazon.com/images/I/71WCtOjU-yL._AC_SY695_.jpg'),
(3, 'Womens Necklace -Pearl', 'Elegant necklace for special occasions.', 49.99, 30, 'Female', 'https://m.media-amazon.com/images/I/61T3xla2UPL._AC_SY695_.jpg');

-- Repeat similar queries for the remaining 35 products

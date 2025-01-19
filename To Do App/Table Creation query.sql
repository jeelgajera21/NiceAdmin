CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);


CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);


CREATE TABLE Tasks (
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    DueDate DATETIME,
    Priority INT DEFAULT 0, -- e.g., 0: Low, 1: Medium, 2: High
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending', -- e.g., 'Pending', 'Completed'
    CategoryID INT NULL FOREIGN KEY REFERENCES Categories(CategoryID),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
	);


	CREATE TABLE Reminders (
    ReminderID INT PRIMARY KEY IDENTITY(1,1),
    TaskID INT NOT NULL FOREIGN KEY REFERENCES Tasks(TaskID),
    ReminderTime DATETIME NOT NULL,
    IsSent BIT NOT NULL DEFAULT 0
);



--Sample Data
-- Users
INSERT INTO Users (Username, Email, PasswordHash) 
VALUES ('john_doe', 'john@example.com', 'hashed_password');

-- Categories
INSERT INTO Categories (UserID, Name, Description) 
VALUES (1, 'Work', 'Tasks related to work'), 
       (1, 'Personal', 'Personal tasks');

-- Tasks
INSERT INTO Tasks (UserID, Title, Description, DueDate, Priority, Status, CategoryID) 
VALUES (1, 'Finish Project', 'Complete the final report', '2024-12-30', 2, 'Pending', 1),
       (1, 'Buy Groceries', 'Milk, Eggs, Bread', '2024-12-23', 0, 'Pending', 2);

-- Reminders
INSERT INTO Reminders (TaskID, ReminderTime) 
VALUES (1, '2024-12-29 10:00:00');

select * from Users
select * from Categories
select * from Tasks
select * from Reminders

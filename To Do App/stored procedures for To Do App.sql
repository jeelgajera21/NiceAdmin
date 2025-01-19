--1. Stored Procedure for Adding a User
CREATE PROCEDURE AddUser
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Users (Username, Email, PasswordHash, CreatedAt, IsActive)
    VALUES (@Username, @Email, @PasswordHash, GETDATE(), 1);
END;
--________________________________________
--2. Stored Procedure for Adding a Task
CREATE PROCEDURE AddTask
    @UserID INT,
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @DueDate DATETIME,
    @Priority INT = 0,
    @CategoryID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Tasks (UserID, Title, Description, DueDate, Priority, Status, CategoryID, CreatedAt, UpdatedAt)
    VALUES (@UserID, @Title, @Description, @DueDate, @Priority, 'Pending', @CategoryID, GETDATE(), GETDATE());
END;
--________________________________________
--3. Stored Procedure for Updating a Task
CREATE PROCEDURE UpdateTask
    @TaskID INT,
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @DueDate DATETIME,
    @Priority INT,
    @Status NVARCHAR(50),
    @CategoryID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Tasks
    SET Title = @Title,
        Description = @Description,
        DueDate = @DueDate,
        Priority = @Priority,
        Status = @Status,
        CategoryID = @CategoryID,
        UpdatedAt = GETDATE()
    WHERE TaskID = @TaskID;
END;
--________________________________________
--4. Stored Procedure for Deleting a Task
CREATE PROCEDURE DeleteTask
    @TaskID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Tasks
    WHERE TaskID = @TaskID;
END;
--________________________________________
--5. Stored Procedure for Fetching Tasks by User
CREATE PROCEDURE GetTasksByUser
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TaskID, Title, Description, DueDate, Priority, Status, CategoryID, CreatedAt, UpdatedAt
    FROM Tasks
    WHERE UserID = @UserID
    ORDER BY DueDate ASC;
END;
--________________________________________
--6. Stored Procedure for Adding a Category
CREATE PROCEDURE AddCategory
    @UserID INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Categories (UserID, Name, Description, CreatedAt)
    VALUES (@UserID, @Name, @Description, GETDATE());
END;
--________________________________________
--7. Stored Procedure for Adding a Reminder
CREATE PROCEDURE AddReminder
    @TaskID INT,
    @ReminderTime DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Reminders (TaskID, ReminderTime, IsSent)
    VALUES (@TaskID, @ReminderTime, 0);
END;
--________________________________________
--8. Stored Procedure for Fetching Tasks with Reminders
CREATE PROCEDURE GetTasksWithReminders
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.TaskID, t.Title, t.Description, t.DueDate, r.ReminderTime, r.IsSent
    FROM Tasks t
    INNER JOIN Reminders r ON t.TaskID = r.TaskID
    WHERE t.UserID = @UserID
    ORDER BY r.ReminderTime ASC;
END;
--________________________________________
--9. Stored Procedure for Updating Task Status
CREATE PROCEDURE UpdateTaskStatus
    @TaskID INT,
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Tasks
    SET Status = @Status,
        UpdatedAt = GETDATE()
    WHERE TaskID = @TaskID;
END;
--________________________________________
--10. Stored Procedure for Deleting a User and Their Data
--Deletes a user and all associated data (tasks, categories, reminders).
CREATE PROCEDURE DeleteUser
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Delete reminders
    DELETE FROM Reminders WHERE TaskID IN (SELECT TaskID FROM Tasks WHERE UserID = @UserID);
    
    -- Delete tasks
    DELETE FROM Tasks WHERE UserID = @UserID;

    -- Delete categories
    DELETE FROM Categories WHERE UserID = @UserID;

    -- Delete user
    DELETE FROM Users WHERE UserID = @UserID;
END;


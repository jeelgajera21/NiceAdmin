------------------------------USER---------------------------------
-- select * from Users
--select user PR_User_SelectAll

ALTER PROCEDURE PR_User_SelectAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT UserID, Username, Email, CreatedAt, IsActive
    FROM Users
	where IsActive <> 0
    ORDER BY CreatedAt DESC;
END 
GO


--select user PR_User_SelectByID 2

CREATE PROCEDURE PR_User_SelectByID
@UserID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT UserID, Username, Email, CreatedAt, IsActive
    FROM Users
	where UserID=@UserID
    ORDER BY CreatedAt DESC;
END 
GO

--select user PR_User_Login 'mascotbot21@gmail.com','M@ScOt21'
--PR_User_Login 'Jeel','MaScOt21'
CREATE PROCEDURE PR_User_Login
   @UsernameOrEmail NVARCHAR(100),
    @PasswordHash NVARCHAR(255)    
	
AS
BEGIN
	SET NOCOUNT ON;
	 SELECT 
         UserID,
         Username,
         Email,
         IsActive
    FROM Users
    WHERE (Username = @UsernameOrEmail OR Email = @UsernameOrEmail) 
      AND PasswordHash = @PasswordHash;


END 
GO

--insert user PR_User_Insert Jeel,'jeelbot21@gmail.com','MaScOt21'

CREATE PROCEDURE PR_User_Insert
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Users (Username, Email, PasswordHash, CreatedAt, IsActive)
    VALUES (@Username, @Email, @PasswordHash, GETDATE(), 1);
END;


--update user PR_User_Update 3,Jeel,'jeelbot21@gmail.com',1

CREATE PROCEDURE PR_User_Update
    @UserID INT,
    @Username NVARCHAR(50),
    @Email NVARCHAR(100),
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Users
    SET Username = @Username,
        Email = @Email,
        IsActive = @IsActive
    WHERE UserID = @UserID;
END;
GO

--delete user PR_User_Delete 1

CREATE PROCEDURE PR_User_Delete
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete related data
    DELETE FROM Reminders WHERE TaskID IN (SELECT TaskID FROM Tasks WHERE UserID = @UserID);
    DELETE FROM Tasks WHERE UserID = @UserID;
    DELETE FROM Categories WHERE UserID = @UserID;

    -- Delete user
    DELETE FROM Users WHERE UserID = @UserID;
END;

--softdelete user PR_User_NotActive 3

CREATE PROCEDURE PR_User_NotActive
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Mark the user as deleted
    UPDATE Users
    SET IsActive = 0
    WHERE UserID = @UserID;
END;

--softrecover user PR_User_Active 2

CREATE PROCEDURE PR_User_Active
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Mark the user as recovered
    UPDATE Users
    SET IsActive = 1
    WHERE UserID = @UserID;
END;

-----------------------------------TASKS-----------------------------------------
--select Tasks PR_Tasks_SelectAll
CREATE PROCEDURE PR_Tasks_SelectAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
    FROM Tasks
	
END 
GO


--select user PR_Tasks_SelectByID 3

ALTER PROCEDURE PR_Tasks_SelectByID
@TaskID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
    FROM Tasks
	where TaskID=@TaskID
    
END 
GO

--select Tasks PR_Tasks_GetTasksByUser 3

CREATE PROCEDURE PR_Tasks_GetTasksByUser
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TaskID, Title, Description, DueDate, Priority, Status, CategoryID, CreatedAt, UpdatedAt
    FROM Tasks
    WHERE UserID = @UserID
    ORDER BY DueDate ASC;
END;



--insert Task PR_Tasks_Insert 3,'task 2','complete task 2','2025-03-31 10:11:15.530',2,5

CREATE PROCEDURE PR_Tasks_Insert
    @UserID INT,
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @DueDate DATETIME,
    @Priority INT,
    @CategoryID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Tasks (UserID, Title, Description, DueDate, Priority, Status, CategoryID, CreatedAt, UpdatedAt)
    VALUES (@UserID, @Title, @Description, @DueDate, @Priority,'Pending', @CategoryID, GETDATE(), GETDATE());
END;

--update Task PR_Tasks_Update 3,'task id is 3','task 1 completed','2025-02-28 10:11:15.530',1,'Partially Completed',4

CREATE PROCEDURE PR_Tasks_Update
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

--delete Tasks PR_Tasks_Delete 6 

CREATE PROCEDURE PR_Tasks_Delete
    @TaskID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete reminders for this task
    DELETE FROM Reminders WHERE TaskID = @TaskID;

    -- Delete the task
    DELETE FROM Tasks WHERE TaskID = @TaskID;
END;




-----------------------------------------CATEGORY-----------------------------------------

--select categories PR_Categories_SelectAll PR_Tasks_SelectAll
Alter PROCEDURE PR_Categories_SelectAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CategoryID, UserID, CategoryName, Description,CreatedAt
    FROM Categories
	
    ORDER BY CategoryID;
END 
GO

--select Categories PR_Categories_SelectByID 4
CREATE PROCEDURE PR_Categories_SelectByID
@CategoryID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CategoryID, UserID, CategoryName, Description,CreatedAt
    FROM Categories
	where CategoryID=@CategoryID
    ORDER BY CategoryID;
END 
GO

--select Categories PR_Categories_GetCategoryByUser 3

CREATE PROCEDURE PR_Categories_GetCategoryByUser
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CategoryID, CategoryName, Description, CreatedAt
    FROM Categories
    WHERE UserID = @UserID
    ORDER BY CreatedAt DESC;
END;


--insert Catogory PR_Categories_Insert 3,'Buy sak','Potato,Tomato'

Alter PROCEDURE PR_Categories_Insert
    @UserID INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Categories (UserID, CategoryName, Description, CreatedAt)
    VALUES (@UserID, @Name, @Description, GETDATE());
END;
GO

--update Categories PR_Categories_Update 4,'Buy Things','Potato,Tomato,Rotato'
CREATE PROCEDURE PR_Categories_Update
    @CategoryID INT,
    @CategoryName NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Categories
    SET CategoryName = @CategoryName,
        Description = @Description
    WHERE CategoryID = @CategoryID;
END;

--delete Catogories PR_Categories_Delete 5
CREATE PROCEDURE PR_Categories_Delete
    @CategoryID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Unlink tasks from this category
    UPDATE Tasks
    SET CategoryID = NULL
    WHERE CategoryID = @CategoryID;

    -- Delete the category
    DELETE FROM Categories WHERE CategoryID = @CategoryID;
END;



----------------------------------------REMINDER--------------------------------------------

--select Reminder PR_Reminders_SelectAll
CREATE PROCEDURE PR_Reminders_SelectAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
    FROM Reminders

END 
GO

--select Reminder PR_Reminders_SelectByID 2
CREATE PROCEDURE PR_Reminders_SelectByID
@ReminderID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
    FROM Reminders
	where ReminderID=@ReminderID
END 
GO

-- select Reminder PR_Reminders_GetRemindersByTask 9
CREATE PROCEDURE PR_Reminders_GetRemindersByTask
    @TaskID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ReminderID, ReminderTime, IsSent
    FROM Reminders
    WHERE TaskID = @TaskID
    ORDER BY ReminderTime ASC;
END;


--insert Reminder PR_Reminders_Insert 9,'2025-01-31 10:11:15.530'

CREATE PROCEDURE PR_Reminders_Insert
    @TaskID INT,
    @ReminderTime DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Reminders (TaskID, ReminderTime, IsSent)
    VALUES (@TaskID, @ReminderTime, 0);
END;

--update Reminder PR_Reminders_Update 2,'2025-03-31 10:11:15.530',1
CREATE PROCEDURE PR_Reminders_Update
    @ReminderID INT,
    @ReminderTime DATETIME,
    @IsSent BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Reminders
    SET ReminderTime = @ReminderTime,
        IsSent = @IsSent
    WHERE ReminderID = @ReminderID;
END;

--delete Reminders PR_Reminders_Delete 2
CREATE PROCEDURE PR_Reminders_Delete
    @ReminderID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Reminders WHERE ReminderID = @ReminderID;
END;


--sentReminder Reminders PR_Reminders_Sent 3

CREATE PROCEDURE PR_Reminders_Sent
    @ReminderID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Mark the Reminder as Sent
    UPDATE Reminders
    SET IsSent = 1
    WHERE ReminderID = @ReminderID;
END;


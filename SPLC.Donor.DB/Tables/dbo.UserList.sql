CREATE TABLE [dbo].[UserList]
(
	[pk_UserList]  INT NOT NULL PRIMARY KEY IDENTITY , 
    [UserName] NVARCHAR(50) NULL, 
    [FullName] NVARCHAR(50) NULL, 
    [Role] INT NULL
)

CREATE TABLE [dbo].[DonorMessages]
(
	[pk_Message] INT NOT NULL PRIMARY KEY IDENTITY , 
    [MessageId] INT NULL, 
    [MessageText] NVARCHAR(50) NULL, 
    [MessageDescription] NVARCHAR(MAX) NULL, 
    [Date_Added] DATETIME2 NULL DEFAULT getDate(), 
    [User_Added] NVARCHAR(50) NULL
)

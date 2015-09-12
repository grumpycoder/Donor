CREATE TABLE [dbo].[DonorEventList]
(
	[pk_DonorEventList] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fk_Event] INT NULL, 
    [fk_DonorList] NVARCHAR(15) NULL, 
    [Date_Added] DATETIME2 NULL,
	[User_Added] [nvarchar](50) NULL,
	[Response_Date] [datetime2](7) NULL,
	[Response_Type] [nvarchar](50) NULL,
	[TicketsRequested] [int] DEFAULT 0,
	[WaitingList_Date] [datetime2](7) NULL,
	[WaitingListOrder] [int] DEFAULT 0,
	[Attending] [bit] NULL DEFAULT 0,
	[TicketsMailed_Date] [datetime2](7) NULL, 
    [TicketsMailed_User] NVARCHAR(50) NULL, 
    [DonorComments] NVARCHAR(MAX) NULL, 
    [SPLCComments] NVARCHAR(MAX) NULL, 
    [UpdatedInfo] BIT NULL DEFAULT 0, 
    [UpdatedInfoDateTime] DATETIME2 NULL, 
    [UpdatedInfo_User] NVARCHAR(255) NULL 
)

CREATE TABLE [dbo].[TaxYear]
(
	[pk_TaxYear] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [TaxYear] INT NOT NULL, 
    [Start_DateTime] SMALLDATETIME NULL, 
    [End_DateTime] SMALLDATETIME NULL, 
    [SupportEmail] VARCHAR(100) NULL, 
    [SupportPhone] VARCHAR(25) NULL
)

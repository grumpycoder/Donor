-- =============================================
-- Author:		Wade McAvoy (Analysis Factory,LLC)
-- Create date: 12/15/14
-- Description:	Load Tax Data from Staging
-- =============================================
CREATE PROCEDURE [dbo].[p_LoadTaxData]
	@TaxYear int
AS
BEGIN
	SET NOCOUNT ON;
	
/*******************************************************
*  If the Tax Year does not exist then create it
*******************************************************/
IF NOT EXISTS (SELECT pk_TaxYear FROM TaxYear WHERE TaxYear=@TaxYear)
BEGIN
	INSERT INTO TaxYear (pk_TaxYear,TaxYear) VALUES (newid(),@TaxYear)
END

/********** End Create Tax Year ***********************/


DECLARE @TaxReceiptId nvarchar(15)

DECLARE Donor_Cursor CURSOR FOR 
SELECT TAX_RECEIPT_ID FROM TaxDataStage

OPEN Donor_Cursor
FETCH NEXT FROM Donor_Cursor
INTO @TaxReceiptId

WHILE @@FETCH_STATUS = 0
BEGIN

	/*********************************************************
	*  If record exists then delete it
	**********************************************************/
	IF EXISTS (SELECT pk_TaxData FROM TaxData WHERE TaxYear=@TaxYear AND TAX_RECEIPT_ID=@TaxReceiptId)
	BEGIN
		DELETE TaxData WHERE TaxYear=@TaxYear AND TAX_RECEIPT_ID=@TaxReceiptId
	END
	/*********************************************************/

	/*********************************************************
	*	Load Record
	*********************************************************/
	IF(@TaxReceiptId <> 'TAX RECEIPT ID')
	BEGIN	
		INSERT INTO TaxData (pk_TaxData,TaxYear, TAX_RECEIPT_ID,CONSTITUENT1,CONSTITUENT2,
		DONATION_DATE_1,AMOUNT_1,
		DONATION_DATE_2,AMOUNT_2,
		DONATION_DATE_3,AMOUNT_3,
		DONATION_DATE_4,AMOUNT_4,
		DONATION_DATE_5,AMOUNT_5,
		DONATION_DATE_6,AMOUNT_6,
		DONATION_DATE_7,AMOUNT_7,
		DONATION_DATE_8,AMOUNT_8,
		DONATION_DATE_9,AMOUNT_9,
		DONATION_DATE_10,AMOUNT_10,
		DONATION_DATE_11,AMOUNT_11,
		DONATION_DATE_12,AMOUNT_12,
		DONATION_DATE_13,AMOUNT_13,
		DONATION_DATE_14,AMOUNT_14,
		DONATION_DATE_15,AMOUNT_15,
		DONATION_DATE_16,AMOUNT_16,
		DONATION_DATE_17,AMOUNT_17,
		DONATION_DATE_18,AMOUNT_18,
		DONATION_DATE_19,AMOUNT_19,
		DONATION_DATE_20,AMOUNT_20,
		DONATION_DATE_21,AMOUNT_21,
		DONATION_DATE_22,AMOUNT_22,
		DONATION_DATE_23,AMOUNT_23,
		DONATION_DATE_24,AMOUNT_24,
		DONATION_DATE_25,AMOUNT_25,
		ADDRESSLINE1,ADDRESSLINE2,ADDRESSLINE3,CITY,STATE,ZIP,COUNTRY)
		(SELECT newid(),@TaxYear, TAX_RECEIPT_ID,CONSTITUENT1,CONSTITUENT2,
		dbo.DtValue(DONATION_DATE_1), dbo.CurValue(AMOUNT_1),
		dbo.DtValue(DONATION_DATE_2), dbo.CurValue(AMOUNT_2),
		dbo.DtValue(DONATION_DATE_3), dbo.CurValue(AMOUNT_3),
		dbo.DtValue(DONATION_DATE_4), dbo.CurValue(AMOUNT_4),
		dbo.DtValue(DONATION_DATE_5), dbo.CurValue(AMOUNT_5),
		dbo.DtValue(DONATION_DATE_6), dbo.CurValue(AMOUNT_6),
		dbo.DtValue(DONATION_DATE_7), dbo.CurValue(AMOUNT_7),
		dbo.DtValue(DONATION_DATE_8), dbo.CurValue(AMOUNT_8),
		dbo.DtValue(DONATION_DATE_9), dbo.CurValue(AMOUNT_9),
		dbo.DtValue(DONATION_DATE_10), dbo.CurValue(AMOUNT_10),
		dbo.DtValue(DONATION_DATE_11), dbo.CurValue(AMOUNT_11),
		dbo.DtValue(DONATION_DATE_12), dbo.CurValue(AMOUNT_12),
		dbo.DtValue(DONATION_DATE_13), dbo.CurValue(AMOUNT_13),
		dbo.DtValue(DONATION_DATE_14), dbo.CurValue(AMOUNT_14),
		dbo.DtValue(DONATION_DATE_15), dbo.CurValue(AMOUNT_15),
		dbo.DtValue(DONATION_DATE_16), dbo.CurValue(AMOUNT_16),
		dbo.DtValue(DONATION_DATE_17), dbo.CurValue(AMOUNT_17),
		dbo.DtValue(DONATION_DATE_18), dbo.CurValue(AMOUNT_18),
		dbo.DtValue(DONATION_DATE_19), dbo.CurValue(AMOUNT_19),
		dbo.DtValue(DONATION_DATE_20), dbo.CurValue(AMOUNT_20),
		dbo.DtValue(DONATION_DATE_21), dbo.CurValue(AMOUNT_21),
		dbo.DtValue(DONATION_DATE_22), dbo.CurValue(AMOUNT_22),
		dbo.DtValue(DONATION_DATE_23), dbo.CurValue(AMOUNT_23),
		dbo.DtValue(DONATION_DATE_24), dbo.CurValue(AMOUNT_24),
		dbo.DtValue(DONATION_DATE_25), dbo.CurValue(AMOUNT_25),
		ADDRESSLINE1,ADDRESSLINE2,ADDRESSLINE3,CITY,STATE,ZIP,COUNTRY
		 FROM TaxDataStage
		 WHERE TAX_RECEIPT_ID=@TaxReceiptId)
	END
	/*********************************************************/

FETCH NEXT FROM Donor_Cursor 
INTO @TaxReceiptId

END
CLOSE Donor_Cursor
DEALLOCATE Donor_Cursor

END
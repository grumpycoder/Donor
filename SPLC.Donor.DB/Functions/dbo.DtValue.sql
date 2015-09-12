-- =============================================
-- Author:		Wade McAvoy (Analysis Factory,LLC)
-- Create date: 12/15/14
-- Description:	Create Date Value
-- =============================================
CREATE FUNCTION [dbo].[DtValue] 
(
	@DValue varchar(100)
)
RETURNS date
AS
BEGIN
	IF(ISDATE(@DValue) = 1)
	BEGIN
		RETURN @DValue
	END
	ELSE
	BEGIN
		RETURN NULL
	END
	
	RETURN NULL
END
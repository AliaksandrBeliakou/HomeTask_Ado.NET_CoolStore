CREATE PROCEDURE [dbo].[GetOrdersByFilter]
	@ProductId int null,
	@Year int null,
	@Month int null,
	@Status nvarchar(50) null
AS
BEGIN
	SELECT * FROM Orders WHERE 
		((YEAR(CreateDate) = @Year or YEAR(UpdateDate) = @Year) or @Year is null)
		and ((MONTH(CreateDate) = @Month or MONTH(UpdateDate) = @Month) or @Month is null)
		and (Status = @Status or @Status is null)
		and (ProductId = @ProductId or @ProductId is null)
END

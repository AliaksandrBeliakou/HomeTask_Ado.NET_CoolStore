CREATE PROCEDURE [dbo].[DeleteOrdersByFilter]
	@ProductId int null,
	@Year int null,
	@Month int null,
	@status nvarchar(50) null
AS
BEGIN
	DELETE FROM Orders WHERE 
		((YEAR(CreateDate) = @Year or YEAR(UpdateDate) = @Year) or @Year is null)
		and ((MONTH(CreateDate) = @Month or MONTH(UpdateDate) = @Month) or @Month is null)
		and (Status = @Status or @Status is null)
		and (ProductId = @ProductId or @ProductId is null)
END


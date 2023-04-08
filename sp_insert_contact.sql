USE [MyTest]
GO
/****** Object:  StoredProcedure [dbo].[insert_contact]    Script Date: 6/21/2022 12:56:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[insert_contact]
	@name nvarchar(50) = null,
	@phone nvarchar(50) = null,
	@email nvarchar(50) = null
AS
BEGIN
	SET NOCOUNT ON;

	insert into Contact(Name, Phone, Email)
	values(@name, @phone, @email)
END

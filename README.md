>**Làm thế nào để hoạt động?**
<p>Đầu tiên cần nhập dữ liệu script bên dưới và trích xuất lên MSSQL để khởi tạo DB</p>

Mã khởi tạo MSSQL:
```
CREATE DATABASE PensDB;

USE [PensDB]
GO
/****** Object:  Table [dbo].[Pens]    Script Date: 10/23/2022 1:56:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pens](
	[PenID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Type] [varchar](50) NULL,
	[Price] [int] NULL,
 CONSTRAINT [PK_Pens] PRIMARY KEY CLUSTERED 
(
	[PenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[PenAddOrEdit]    Script Date: 10/23/2022 1:56:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PenAddOrEdit]
	@PenID INT,
	@Title VARCHAR(50),
	@Type VARCHAR(50),
	@Price INT
AS
BEGIN

	SET NOCOUNT ON;

	IF @PenID = 0
	BEGIN
		INSERT INTO Pens(Title, Type, Price)
		VALUES(@Title, @Type, @Price)
	END
	ELSE
	BEGIN
		UPDATE Pens
		SET
			Title=@Title,
			Type=@Type,
			Price=@Price
		WHERE PenID =@PenID
	END
END
GO
/****** Object:  StoredProcedure [dbo].[PenDeleteByID]    Script Date: 10/23/2022 1:56:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PenDeleteByID]
	@PenID INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE Pens
	WHERE PenID = @PenID
END
GO
/****** Object:  StoredProcedure [dbo].[PenViewAll]    Script Date: 10/23/2022 1:56:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
CREATE PROCEDURE [dbo].[PenViewAll]

AS
BEGIN

	SET NOCOUNT ON;

	SELECT *
	FROM Pens
END
GO
/****** Object:  StoredProcedure [dbo].[PenViewByID]    Script Date: 10/23/2022 1:56:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PenViewByID]
	@PenID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM Pens
	WHERE PenID = @PenID
END
GO

```

Chỉnh sửa đường dẫn kết nối DB trong tập tin **appsettings.json** có dạng tương ứng:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DevConnection": "Data Source=DESKTOP-KOTO;Initial Catalog=PensDB;Integrated Security=True",
    "MVCCRUDwoEFContext": "Server=(localdb)\\mssqllocaldb;Database=MVCCRUDwoEF.Data;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

Chú ý thay đổi **Data Source=DESKTOP-KOTO** theo tên máy đang sử dụng 
>Hoàn thành và chạy thử

>Nguồn tham khảo: CodAffection

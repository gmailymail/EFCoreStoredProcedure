/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 5/1/2021 3:00:17 AM ******/

--Execute this Script after the DB Creation
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetProducts]
as
select pr.Id, br.Id BrandId, pr.[Name], br.Name [BrandName] from Brands br inner join Products pr
on br.Id=pr.BrandId


GO

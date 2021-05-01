
/****** Object:  StoredProcedure [dbo].[GetProductsByBrandId]    Script Date: 5/1/2021 3:01:59 AM ******/
--Execute this Script after the DB Creation
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[GetProductsByBrandId]
(
@BrandId int
)
as
select pr.Id, br.Id BrandId, pr.Name, br.Name [BrandName] from Brands br inner join Products pr
on br.Id=pr.BrandId
WHERE BR.Id=@BrandId
GO



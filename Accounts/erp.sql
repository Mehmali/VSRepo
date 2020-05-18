 

CREATE TABLE [dbo].[MenuPermission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Emp_Code] [varchar](50) NOT NULL,
	[MenuId] [int] NOT NULL,
 CONSTRAINT [PK_MenuPermission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

create Procedure [dbo].[Sp_UserInfoUpdate]
@UserID int,
@Deleted bit=null,
@RoleID int=null,
@Username varchar(50)=null,
@Password varchar(15)=null,
@EmailAddress varchar(50)=null
AS
BEGIN

if @Username is not null
begin
UPDATE UserInfo SET 
[Username] = @Username, [Password] = @Password, [Deleted] =@Deleted, [RoleID] =@RoleID,[EmailAddress]=@EmailAddress 
WHERE [UserID] = @UserID
end
else
begin
UPDATE UserInfo SET 
[Password] = @Password where   [UserID] = @UserID
end
END

 



CREATE TABLE [dbo].[Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [varchar](150) NOT NULL,
	[ControllerName] [varchar](50) NULL,
	[MenuDesc] [varchar](500) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

 


CREATE TABLE [dbo].[EmployeeRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpCode] [varchar](20) NOT NULL,
	[Role] [int] NOT NULL,
	[IsAdmin] [int] NULL,
 CONSTRAINT [PK_EmployeeRole] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO




create PROCEDURE [dbo].[IsAllowToAccessMenu]          
 -- Add the parameters for the stored procedure here          
@UserID INT,          
@MenuName varchar(500),   
@ControllerName varchar(500)          
AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
           
 DECLARE @IsAllow AS Bit          
 SET @IsAllow=0          
       
 DECLARE @IsSuperAdmin AS Bit          
 SET @IsSuperAdmin=0          

--Enter New menue 
IF not (EXISTS (SELECT Id from menu where menuname =@MenuName and controllername=@ControllerName))    
	   begin
	     insert into menu(menuname,controllername)values(@MenuName,@ControllerName)
	   end
--end of enter new menue


 IF EXISTS (SELECT ID FROM EmployeeRole where ID = @UserID AND [ROLE] = 1)          
  BEGIN          
   SET @IsAllow=1       
   SET @IsSuperAdmin = 1         
  END       
 ELSE IF   EXISTS (SELECT e.ID FROM EmployeeRole e   
     INNER JOIN MENUPERMISSION  m on m.Emp_code = e.EmpCode   
     INNER JOIN Menu mm ON m.menuid=mm.id  
     where EmpCode = @UserID AND ISADMIN = 1 AND  mm.ControllerName=@ControllerName  
       )    
   
 --ELSE IF   EXISTS (SELECT e.ID FROM EmployeeRole e   
 --  where EmpCode = @EmpCode AND ISADMIN = 1)         
  BEGIN          
    SET @IsAllow=1        
  END       
 ELSE       
  BEGIN      
   IF EXISTS (SELECT mp.ID FROM MENUPERMISSION mp          
   INNER JOIN Menu m ON mp.menuid=m.id WHERE m.MenuName=@MenuName  and Emp_Code = @UserID AND  m.ControllerName=@ControllerName )          
    BEGIN          
    SET @IsAllow=1          
    END          
 END      
 SELECT @IsAllow AS IsAllow ,@IsSuperAdmin AS IsSuperAdmin         
          
END   
  
  
  
 
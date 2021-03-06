CREATE TABLE [dbo].[Person] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [LastName]       NVARCHAR (50)  NOT NULL,
    [FirstName]      NVARCHAR (50)  NOT NULL,
    [HireDate]       DATETIME       NULL,
    [EnrollmentDate] DATETIME       NULL,
    CONSTRAINT [PK_dbo.Person] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Person]
           ([LastName]
           ,[FirstName]
           )
     VALUES
           ('Bob'
           ,'Jim')
GO

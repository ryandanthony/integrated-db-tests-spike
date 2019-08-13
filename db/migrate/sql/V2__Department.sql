CREATE TABLE [dbo].[Department] (
    [DepartmentID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NULL,
    [Budget]       MONEY         NOT NULL,
    [StartDate]    DATETIME      NOT NULL,
    [RowVersion]   ROWVERSION    NOT NULL,
    CONSTRAINT [PK_dbo.Department] PRIMARY KEY CLUSTERED ([DepartmentID] ASC),
);
/*
 *
 *      CRIAÇÃO DAS TABELAS 
 *
 */

-- APPLICATION
CREATE TABLE [dbo].[Application] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (40) NOT NULL,
    [Creation_DT] DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

-- CONTAINER
CREATE TABLE [dbo].[Container] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (40) NOT NULL,
    [Creation_DT]    DATETIME     NOT NULL,
    [Application_id] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC, [Application_id] ASC),
    CONSTRAINT [FK_ApplicationID] FOREIGN KEY ([Application_id]) REFERENCES [dbo].[Application] ([Id]) ON DELETE CASCADE
);

-- DATA
CREATE TABLE [dbo].[Data] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (40)    NOT NULL,
    [Content]      VARBINARY (MAX) NULL,
    [Creation_DT]  DATETIME        NOT NULL,
    [Container_id] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC, [Container_id] ASC),
    CONSTRAINT [FK_ContainerID] FOREIGN KEY ([Container_id]) REFERENCES [dbo].[Container] ([Id]) ON DELETE CASCADE
);

-- SUBSCRIPTION
CREATE TABLE [dbo].[Subscription] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (40)  NOT NULL,
    [Creation_DT]  DATETIME      NOT NULL,
    [Container_id] INT           NOT NULL,
    [Event]        INT           NOT NULL,
    [Endpoint]     VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC, [Container_id] ASC),
    CONSTRAINT [FK_Subscription_ContainerID] FOREIGN KEY ([Container_id]) REFERENCES [dbo].[Container] ([Id]) ON DELETE CASCADE,
    CHECK ([Event]=(2) OR [Event]=(1) OR [Event]=(3))
);

/*
 *
 *      POPULAÇÃO DAS TABELAS COM DADOS DE TESTE
 *
 */





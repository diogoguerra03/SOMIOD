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

-- APPLICATION
SET IDENTITY_INSERT [dbo].[Application] ON
INSERT INTO [dbo].[Application] ([Id], [Name], [Creation_DT]) VALUES (218, N'Lighting', N'2023-12-27 22:40:32')
INSERT INTO [dbo].[Application] ([Id], [Name], [Creation_DT]) VALUES (237, N'Television', N'2023-12-29 20:52:11')
INSERT INTO [dbo].[Application] ([Id], [Name], [Creation_DT]) VALUES (256, N'Microwave', N'2024-01-01 12:13:14')
INSERT INTO [dbo].[Application] ([Id], [Name], [Creation_DT]) VALUES (240, N'Garage', N'2024-01-06 16:10:07')
SET IDENTITY_INSERT [dbo].[Application] OFF

-- CONTAINER
SET IDENTITY_INSERT [dbo].[Container] ON
INSERT INTO [dbo].[Container] ([Id], [Name], [Creation_DT], [Application_id]) VALUES (180, N'light_bulb', N'2023-12-27 22:40:34', 218)
INSERT INTO [dbo].[Container] ([Id], [Name], [Creation_DT], [Application_id]) VALUES (190, N'television_screen', N'2023-12-29 20:52:13', 237)
INSERT INTO [dbo].[Container] ([Id], [Name], [Creation_DT], [Application_id]) VALUES (192, N'microwave_plate', N'2024-01-01 12:13:16', 256)
INSERT INTO [dbo].[Container] ([Id], [Name], [Creation_DT], [Application_id]) VALUES (199, N'garage_door', N'2024-01-06 16:10:09', 240)
SET IDENTITY_INSERT [dbo].[Container] OFF

-- DATA
SET IDENTITY_INSERT [dbo].[Data] ON
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (54, N'on1703716841420', <Binary Data>, N'2023-12-27 22:40:41', 180)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (55, N'off1703716842926', <Binary Data>, N'2023-12-27 22:40:42', 180)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (57, N'on1703716955934', <Binary Data>, N'2023-12-27 22:42:35', 180)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (59, N'abrir1704557413400', <Binary Data>, N'2024-01-06 16:10:13', 199)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (60, N'fechar1704557414529', <Binary Data>, N'2024-01-06 16:10:14', 199)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (61, N'abrir1704557416737', <Binary Data>, N'2024-01-06 16:10:16', 199)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (62, N'fechar1704557417732', <Binary Data>, N'2024-01-06 16:10:17', 199)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (63, N'abrir1704557418563', <Binary Data>, N'2024-01-06 16:10:18', 199)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (64, N'fechar1704557419371', <Binary Data>, N'2024-01-06 16:10:19', 199)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (65, N'abrir1704557420202', <Binary Data>, N'2024-01-06 16:10:20', 199)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (66, N'abrir1704557421073', <Binary Data>, N'2024-01-06 16:10:21', 199)
INSERT INTO [dbo].[Data] ([Id], [Name], [Content], [Creation_DT], [Container_id]) VALUES (67, N'abrir1704557422444', <Binary Data>, N'2024-01-06 16:10:22', 199)
SET IDENTITY_INSERT [dbo].[Data] OFF


-- SUBSCRIPTION
SET IDENTITY_INSERT [dbo].[Subscription] ON
INSERT INTO [dbo].[Subscription] ([Id], [Name], [Creation_DT], [Container_id], [Event], [Endpoint]) VALUES (182, N'sub1', N'2023-12-27 22:40:35', 180, 1, N'mqtt://127.0.0.1')
INSERT INTO [dbo].[Subscription] ([Id], [Name], [Creation_DT], [Container_id], [Event], [Endpoint]) VALUES (191, N'mqttTeste', N'2023-12-29 14:16:26', 180, 1, N'mqtt://127.0.0.1')
INSERT INTO [dbo].[Subscription] ([Id], [Name], [Creation_DT], [Container_id], [Event], [Endpoint]) VALUES (199, N'httpTeste', N'2023-12-29 20:12:38', 180, 1, N'http://127.0.0.1')
INSERT INTO [dbo].[Subscription] ([Id], [Name], [Creation_DT], [Container_id], [Event], [Endpoint]) VALUES (205, N'sub1', N'2024-01-06 16:10:09', 199, 1, N'mqtt://127.0.0.1')
SET IDENTITY_INSERT [dbo].[Subscription] OFF

/*
Navicat SQL Server Data Transfer

Source Server         : DB Dev Pusintek
Source Server Version : 110000
Source Host           : 10.242.89.23:1433
Source Database       : ECorpPortalHangfire
Source Schema         : HangFire

Target Server Type    : SQL Server
Target Server Version : 110000
File Encoding         : 65001

Date: 2018-10-26 22:13:35
*/


-- ----------------------------
-- Table structure for AggregatedCounter
-- ----------------------------
DROP TABLE [HangFire].[AggregatedCounter]
GO
CREATE TABLE [HangFire].[AggregatedCounter] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Key] nvarchar(100) NOT NULL ,
[Value] bigint NOT NULL ,
[ExpireAt] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[HangFire].[AggregatedCounter]', RESEED, 49)
GO

-- ----------------------------
-- Table structure for Counter
-- ----------------------------
DROP TABLE [HangFire].[Counter]
GO
CREATE TABLE [HangFire].[Counter] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Key] nvarchar(100) NOT NULL ,
[Value] smallint NOT NULL ,
[ExpireAt] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[HangFire].[Counter]', RESEED, 153)
GO

-- ----------------------------
-- Table structure for Hash
-- ----------------------------
DROP TABLE [HangFire].[Hash]
GO
CREATE TABLE [HangFire].[Hash] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Key] nvarchar(100) NOT NULL ,
[Field] nvarchar(100) NOT NULL ,
[Value] nvarchar(MAX) NULL ,
[ExpireAt] datetime2(7) NULL 
)


GO
DBCC CHECKIDENT(N'[HangFire].[Hash]', RESEED, 8)
GO

-- ----------------------------
-- Table structure for Job
-- ----------------------------
DROP TABLE [HangFire].[Job]
GO
CREATE TABLE [HangFire].[Job] (
[Id] int NOT NULL IDENTITY(1,1) ,
[StateId] int NULL ,
[StateName] nvarchar(20) NULL ,
[InvocationData] nvarchar(MAX) NOT NULL ,
[Arguments] nvarchar(MAX) NOT NULL ,
[CreatedAt] datetime NOT NULL ,
[ExpireAt] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[HangFire].[Job]', RESEED, 52)
GO

-- ----------------------------
-- Table structure for JobParameter
-- ----------------------------
DROP TABLE [HangFire].[JobParameter]
GO
CREATE TABLE [HangFire].[JobParameter] (
[Id] int NOT NULL IDENTITY(1,1) ,
[JobId] int NOT NULL ,
[Name] nvarchar(40) NOT NULL ,
[Value] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[HangFire].[JobParameter]', RESEED, 121)
GO

-- ----------------------------
-- Table structure for JobQueue
-- ----------------------------
DROP TABLE [HangFire].[JobQueue]
GO
CREATE TABLE [HangFire].[JobQueue] (
[Id] int NOT NULL IDENTITY(1,1) ,
[JobId] int NOT NULL ,
[Queue] nvarchar(50) NOT NULL ,
[FetchedAt] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[HangFire].[JobQueue]', RESEED, 51)
GO

-- ----------------------------
-- Table structure for List
-- ----------------------------
DROP TABLE [HangFire].[List]
GO
CREATE TABLE [HangFire].[List] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Key] nvarchar(100) NOT NULL ,
[Value] nvarchar(MAX) NULL ,
[ExpireAt] datetime NULL 
)


GO

-- ----------------------------
-- Table structure for Schema
-- ----------------------------
DROP TABLE [HangFire].[Schema]
GO
CREATE TABLE [HangFire].[Schema] (
[Version] int NOT NULL 
)


GO

-- ----------------------------
-- Table structure for Server
-- ----------------------------
DROP TABLE [HangFire].[Server]
GO
CREATE TABLE [HangFire].[Server] (
[Id] nvarchar(100) NOT NULL ,
[Data] nvarchar(MAX) NULL ,
[LastHeartbeat] datetime NOT NULL 
)


GO

-- ----------------------------
-- Table structure for Set
-- ----------------------------
DROP TABLE [HangFire].[Set]
GO
CREATE TABLE [HangFire].[Set] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Key] nvarchar(100) NOT NULL ,
[Score] float(53) NOT NULL ,
[Value] nvarchar(256) NOT NULL ,
[ExpireAt] datetime NULL 
)


GO
DBCC CHECKIDENT(N'[HangFire].[Set]', RESEED, 38)
GO

-- ----------------------------
-- Table structure for State
-- ----------------------------
DROP TABLE [HangFire].[State]
GO
CREATE TABLE [HangFire].[State] (
[Id] int NOT NULL IDENTITY(1,1) ,
[JobId] int NOT NULL ,
[Name] nvarchar(20) NOT NULL ,
[Reason] nvarchar(100) NULL ,
[CreatedAt] datetime NOT NULL ,
[Data] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[HangFire].[State]', RESEED, 190)
GO

-- ----------------------------
-- Indexes structure for table AggregatedCounter
-- ----------------------------
CREATE UNIQUE INDEX [UX_HangFire_CounterAggregated_Key] ON [HangFire].[AggregatedCounter]
([Key] ASC) 
INCLUDE ([Value]) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Primary Key structure for table AggregatedCounter
-- ----------------------------
ALTER TABLE [HangFire].[AggregatedCounter] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Counter
-- ----------------------------
CREATE INDEX [IX_HangFire_Counter_Key] ON [HangFire].[Counter]
([Key] ASC) 
INCLUDE ([Value]) 
GO

-- ----------------------------
-- Primary Key structure for table Counter
-- ----------------------------
ALTER TABLE [HangFire].[Counter] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Hash
-- ----------------------------
CREATE UNIQUE INDEX [UX_HangFire_Hash_Key_Field] ON [HangFire].[Hash]
([Key] ASC, [Field] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [IX_HangFire_Hash_ExpireAt] ON [HangFire].[Hash]
([ExpireAt] ASC) 
INCLUDE ([Id]) 
GO
CREATE INDEX [IX_HangFire_Hash_Key] ON [HangFire].[Hash]
([Key] ASC) 
INCLUDE ([ExpireAt]) 
GO

-- ----------------------------
-- Primary Key structure for table Hash
-- ----------------------------
ALTER TABLE [HangFire].[Hash] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Job
-- ----------------------------
CREATE INDEX [IX_HangFire_Job_StateName] ON [HangFire].[Job]
([StateName] ASC) 
GO
CREATE INDEX [IX_HangFire_Job_ExpireAt] ON [HangFire].[Job]
([ExpireAt] ASC) 
INCLUDE ([Id]) 
GO

-- ----------------------------
-- Primary Key structure for table Job
-- ----------------------------
ALTER TABLE [HangFire].[Job] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table JobParameter
-- ----------------------------
CREATE INDEX [IX_HangFire_JobParameter_JobIdAndName] ON [HangFire].[JobParameter]
([JobId] ASC, [Name] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table JobParameter
-- ----------------------------
ALTER TABLE [HangFire].[JobParameter] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table JobQueue
-- ----------------------------
CREATE INDEX [IX_HangFire_JobQueue_QueueAndFetchedAt] ON [HangFire].[JobQueue]
([Queue] ASC, [FetchedAt] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table JobQueue
-- ----------------------------
ALTER TABLE [HangFire].[JobQueue] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table List
-- ----------------------------
CREATE INDEX [IX_HangFire_List_ExpireAt] ON [HangFire].[List]
([ExpireAt] ASC) 
INCLUDE ([Id]) 
GO
CREATE INDEX [IX_HangFire_List_Key] ON [HangFire].[List]
([Key] ASC) 
INCLUDE ([ExpireAt], [Value]) 
GO

-- ----------------------------
-- Primary Key structure for table List
-- ----------------------------
ALTER TABLE [HangFire].[List] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Schema
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Schema
-- ----------------------------
ALTER TABLE [HangFire].[Schema] ADD PRIMARY KEY ([Version])
GO

-- ----------------------------
-- Indexes structure for table Server
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Server
-- ----------------------------
ALTER TABLE [HangFire].[Server] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Set
-- ----------------------------
CREATE UNIQUE INDEX [UX_HangFire_Set_KeyAndValue] ON [HangFire].[Set]
([Key] ASC, [Value] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [IX_HangFire_Set_ExpireAt] ON [HangFire].[Set]
([ExpireAt] ASC) 
INCLUDE ([Id]) 
GO
CREATE INDEX [IX_HangFire_Set_Key] ON [HangFire].[Set]
([Key] ASC) 
INCLUDE ([ExpireAt], [Value]) 
GO

-- ----------------------------
-- Primary Key structure for table Set
-- ----------------------------
ALTER TABLE [HangFire].[Set] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table State
-- ----------------------------
CREATE INDEX [IX_HangFire_State_JobId] ON [HangFire].[State]
([JobId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table State
-- ----------------------------
ALTER TABLE [HangFire].[State] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Foreign Key structure for table [HangFire].[JobParameter]
-- ----------------------------
ALTER TABLE [HangFire].[JobParameter] ADD FOREIGN KEY ([JobId]) REFERENCES [HangFire].[Job] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [HangFire].[State]
-- ----------------------------
ALTER TABLE [HangFire].[State] ADD FOREIGN KEY ([JobId]) REFERENCES [HangFire].[Job] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

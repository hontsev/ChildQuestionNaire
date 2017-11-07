/*
Navicat SQL Server Data Transfer

Source Server         : 202.112.137.13
Source Server Version : 100000
Source Host           : 202.112.137.13:1433
Source Database       : childqn
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 100000
File Encoding         : 65001

Date: 2017-06-02 19:00:02
*/


-- ----------------------------
-- Table structure for answer
-- ----------------------------
DROP TABLE [answer]
GO
CREATE TABLE [answer] (
[id] int NOT NULL IDENTITY(1,1) ,
[answer] nvarchar(MAX) NULL ,
[belong_user] nvarchar(50) NULL ,
[belong_q] int NULL 
)


GO
DBCC CHECKIDENT(N'[answer]', RESEED, 52)
GO

-- ----------------------------
-- Records of answer
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [answer] ON
GO
INSERT INTO [answer] ([id], [answer], [belong_user], [belong_q]) VALUES (N'6', N'3', N'::1', N'67'), (N'7', N'2', N'::1', N'65'), (N'8', N'1', N'::1', N'78'), (N'9', N'1', N'::1', N'79'), (N'10', N'1', N'::1', N'80'), (N'11', N'2', N'::1', N'81'), (N'12', N'2', N'::1', N'82'), (N'13', N'1', N'::1', N'68'), (N'14', N'2', N'::1', N'69'), (N'15', N'1', N'::1', N'70'), (N'16', N'2', N'::1', N'71'), (N'17', N'1', N'::1', N'72'), (N'18', N'2', N'::1', N'73'), (N'19', N'2', N'::1', N'74'), (N'20', N'2', N'::1', N'75'), (N'21', N'2', N'::1', N'76'), (N'22', N'1', N'::1', N'77'), (N'23', N'2', N'::1', N'78'), (N'24', N'2', N'::1', N'79'), (N'25', N'2', N'::1', N'80'), (N'26', N'3', N'::1', N'81'), (N'27', N'2', N'::1', N'82'), (N'28', N'1', N'::1', N'68'), (N'29', N'1', N'::1', N'69'), (N'30', N'2', N'::1', N'70'), (N'31', N'2', N'::1', N'71'), (N'32', N'1', N'::1', N'72'), (N'33', N'2', N'::1', N'73'), (N'34', N'1', N'::1', N'74'), (N'35', N'2', N'::1', N'75'), (N'36', N'1', N'::1', N'76'), (N'37', N'1', N'::1', N'77'), (N'38', N'1', N'::1', N'93'), (N'39', N'2', N'::1', N'94'), (N'40', N'3', N'::1', N'95'), (N'41', N'2', N'::1', N'96'), (N'42', N'1', N'::1', N'97'), (N'43', N'2', N'::1', N'83'), (N'44', N'3', N'::1', N'84'), (N'45', N'2', N'::1', N'85'), (N'46', N'1', N'::1', N'86'), (N'47', N'2', N'::1', N'87'), (N'48', N'3', N'::1', N'88'), (N'49', N'3', N'::1', N'89'), (N'50', N'3', N'::1', N'90'), (N'51', N'1', N'::1', N'91'), (N'52', N'1', N'::1', N'92')
GO
GO
SET IDENTITY_INSERT [answer] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for question
-- ----------------------------
DROP TABLE [question]
GO
CREATE TABLE [question] (
[id] int NOT NULL IDENTITY(1,1) ,
[introduction] nvarchar(MAX) NULL ,
[type] nvarchar(10) NULL ,
[options] nvarchar(MAX) NULL ,
[scores] nvarchar(MAX) NULL ,
[belong_qn] int NULL ,
[age] nvarchar(MAX) NULL 
)


GO
DBCC CHECKIDENT(N'[question]', RESEED, 97)
GO

-- ----------------------------
-- Records of question
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [question] ON
GO
INSERT INTO [question] ([id], [introduction], [type], [options], [scores], [belong_qn], [age]) VALUES (N'68', N'离耳15厘米处摇动内装20粒黄豆的塑料瓶时', N'逻辑能力', N'转头眨眼,张口,不动', N'3,2,1', N'13', N'5岁以下'), (N'69', N'第一次注视离眼20厘米模拟妈妈面孔的黑白图画', N'逻辑能力', N'10秒以上,7秒以上,5秒以下', N'3,2,1', N'13', N'5岁以下'), (N'70', N'分辨声音', N'逻辑能力', N'父母唱歌时转头看唱歌的人,只会看着妈妈唱歌,大人唱歌时眼睛乱看', N'3,2,1', N'13', N'5岁以下'), (N'71', N'牵铃的绳子套在某一肢体上', N'逻辑能力', N'知道动哪一肢体会使铃打响,全身滚动使铃响,不会牵绳弄不出声音', N'3,2,1', N'13', N'5岁以下'), (N'72', N'认识数字和汉字', N'逻辑能力', N'汉字20个、数字4个,汉字10个、数字3个,汉字3个、数字1个', N'3,2,1', N'13', N'5岁以下'), (N'73', N'宝宝会回答简单的问话吗？例如：妈妈问“爸爸上哪去了？”，会回答“上班了”。', N'语言能力', N'经常，而且准确,基本能表达清楚,回答模糊或不能', N'3,2,1', N'13', N'5岁以下'), (N'74', N' 宝宝开始能模仿大人说话。比如：大人说：“洗洗手，要吃饭了！”宝宝会模仿说“吃饭了”。', N'语言能力', N'经常,偶尔,从来不', N'3,2,1', N'13', N'5岁以下'), (N'75', N'背诵儿歌', N'语言能力', N'完整2首,完整1首,1～2句', N'3,2,1', N'13', N'5岁以下'), (N'76', N'认识“1” 会用食指表示要一件东西,或回答“我一岁”', N'语言能力', N'能两次分别要到1件不同的东西，回答“我一岁”,要到1件东西，回答“我一岁”,不会或只会回答“我一岁”', N'3,2,1', N'13', N'5岁以下'), (N'77', N'大人诱导宝宝发“爸”“妈”字音。通过标准：会发爸妈字音，但字音不一定准。', N'语言能力', N'能发,基本可以,不能', N'3,2,1', N'13', N'5岁以下'), (N'78', N'大人拿一件上衣给宝宝穿衣。通过标准：能够伸出手来配合穿衣', N'理解能力', N'能配合,偶尔配合,不能', N'3,2,1', N'13', N'5岁以下'), (N'79', N'对自己喜欢的乐曲', N'理解能力', N'表现出异常兴奋,听到后会比较高兴,没反应', N'3,2,1', N'13', N'5岁以下'), (N'80', N'听儿歌时', N'理解能力', N'能模仿儿歌音调“唱歌”，歌声连贯,会咿咿呀呀地歌唱,不会唱歌', N'3,2,1', N'13', N'5岁以下'), (N'81', N'让宝宝指出3种以上五官的位置。通过标准：能够正确指出3种以上人的五官', N'理解能力', N'能认识三种以上,少于三种,不认识', N'3,2,1', N'13', N'5岁以下'), (N'82', N'展示一些动物图片给宝宝看。通过标准：宝宝能从中认识3种以上动物', N'理解能力', N'能认识三种以上,少于三种,不认识', N'3,2,1', N'13', N'5岁以下'), (N'83', N'以问题的形式让宝宝回答20以内的加减。通过标准：会做20以内的加减法算术', N'逻辑能力', N'比较容易做到,偶尔算错,经常算错', N'3,2,1', N'13', N'5岁及以上'), (N'84', N'大人把钟表分别调出不同的时间，让宝宝看钟表。通过标准：宝宝能够认识整点和几点半，几点一刻，差几分几点。', N'逻辑能力', N'很容易和准确认识,偶尔会认错,不认识', N'3,2,1', N'13', N'5岁及以上'), (N'85', N'分别用10元兑几中5元，几张2元几张1元，买8角钱的东西付10元，应找回多少？通过标准：会兑换20以内的零钱。', N'逻辑能力', N'很容易做到,偶尔会出错,做不到', N'3,2,1', N'13', N'5岁及以上'), (N'86', N'小公共汽车 小公共从起点上来7人，第一站下1人，上5人，第二站，下3人，上8人，第三站下6人，上7人，第四站，下8人，上1人，第五站到终点，问下来几人?', N'逻辑能力', N'会算到第四站，知道终点下来10人,会算到第三站,会算到第二站', N'3,2,1', N'13', N'5岁及以上'), (N'87', N'排队（1）长颈鹿排在动物队伍中，是从左边数的第6，从右边数的第4，问队伍共有几个动物?（2）李强比陈亮高，刘梅比李强高，张月介于刘梅与李强之间，王楠又在李强与陈亮之间，阿秀比刘梅还高，请宝宝替他们按高矮排队 ', N'逻辑能力', N'3分钟做2题,5分钟做2题,会做1题或会做2题，共用8分钟', N'3,2,1', N'13', N'5岁及以上'), (N'88', N'让宝宝叙述前一天或近日参加过的一次有趣活动。通过标准：宝宝叙述细而生动', N'语言能力', N'很叙述细而生动,比较叙述和生动,比较模糊', N'3,2,1', N'13', N'5岁及以上'), (N'89', N'大人先说一词，如大山—山峰—峰顶—顶楼—楼房—房客—客人—人家—家庭。通过标准：宝宝能够接词10个以上', N'语言能力', N'10个以上,5-8个,少于4个', N'3,2,1', N'13', N'5岁及以上'), (N'90', N'宝宝会自己讲述出多少成语', N'语言能力', N'5种 ,4种,2-3种', N'3,2,1', N'13', N'5岁及以上'), (N'91', N'讲自己的好朋友，让孩子讲自己的妈妈或其他人（ 姓名、性别、年龄、工作或事业特点、事例 ）', N'语言能力', N'讲6种以上,讲5-6种,讲3-4种', N'3,2,1', N'13', N'5岁及以上'), (N'92', N'宝宝举出几对同义词', N'语言能力', N'3对,2对,不会或1对', N'3,2,1', N'13', N'5岁及以上'), (N'93', N'人物分析：拿《白雪公主》里的人物进行评价，每个角色能总结出几个特点', N'理解能力', N'3个以上,2个,1个', N'3,2,1', N'13', N'5岁及以上'), (N'94', N'宝宝对一个简单成语理解的程度，如一干二净，三心二意', N'理解能力', N'能理解并可以经常在日常生活中用到,能理解但生活中很少用到,不能理解', N'3,2,1', N'13', N'5岁及以上'), (N'95', N'大人给宝宝一个故事的线索，宝宝能不能维绕这个线索自己编一个故事', N'理解能力', N'能比较清析的编出而且不会跑题,能编出，但很容易跑题,在大人的引导下编出', N'3,2,1', N'13', N'5岁及以上'), (N'96', N'在看完一部动画片或是漫画后宝宝能不能对大人或是其他小朋友说出其中的主要内容(故事情节)', N'理解能力', N'能很清析讲出,比较清析的讲出,比较模糊', N'3,2,1', N'13', N'5岁及以上'), (N'97', N'宝宝对于“对于错”、“好与坏”等一些对立面的现像区分的程度', N'理解能力', N'能区分，也知道该怎么做,能区分，但行为上比较模,不能区分', N'3,2,1', N'13', N'5岁及以上')
GO
GO
SET IDENTITY_INSERT [question] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for questionnaire
-- ----------------------------
DROP TABLE [questionnaire]
GO
CREATE TABLE [questionnaire] (
[id] int NOT NULL IDENTITY(1,1) ,
[title] nvarchar(200) NULL ,
[introduction] nvarchar(MAX) NULL ,
[publish_code] nvarchar(50) NULL 
)


GO
DBCC CHECKIDENT(N'[questionnaire]', RESEED, 14)
GO

-- ----------------------------
-- Records of questionnaire
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [questionnaire] ON
GO
INSERT INTO [questionnaire] ([id], [title], [introduction], [publish_code]) VALUES (N'13', N'儿童综合能力水平测试', N'1 	    充分认识家庭教育的重要性。成功的家庭教育可以开发儿童智力，为更好地掌握知识奠定基础，从某种意义上讲，人的命运几乎取决于家庭环境和教育。
2	    破除“天才”论(即遗传决定论)。遗传素质对人的智慧发展是有影响的，但不是决定因素。“天才”不是生而知之，而是学而知之，是靠后天教育影响形成的。如卡尔.威特，生下来时先天素质低下，在其父母精心培养下，14岁大学毕业，16岁研究生院毕业，被柏林大学聘为大学教授。
3	    不要拔苗助长。要从儿童年龄特点出发，遵循儿童智力发展规律，循序渐进， 因势利导，不可超越孩子智力发展的可能性。
4	    要有恒心。人才的培养周期性长，不可一劳永逸。若是“三天打渔，两天晒网”，是培养不出人才来的，必须持之以恒。
5	    智力发展与非智力因素的发展是不能分开的。如学习兴趣、意志、性格是智力发展的动力，并能弥补智力的不足，改善大脑的工作状态。
6	    正确看待智力测试，正规的智力测试是指经过专门训练的研究人员采用标准化的测验量表对人的智力水平进行科学测量的一个过程。但是，智商并非绝对。如果测评结果不理想，家长也不要着急,因为所得测评结果以及建议,是取同龄儿童、大众标准，是作为家长在平时教育宝宝的一个辅助教材和参考。其次，一个人的智商会随年龄而改变。有些孩子智商较低是因健康欠佳、环境不良、教育不当所致，改变这些因素，智商便有可能提高。即使智商正常的儿童，也会发生学习困难，比如孩子对学习缺乏兴趣等。因此，父母绝不能对智商高的孩子掉以轻心，对智商较低的孩子灰心丧气，应对孩子作全面的研究，找出影响智商的不利因素，采取有效措施，促进孩子智力的提高。最重要的还是要靠家长平时多和宝宝多沟通交流、做游戏，以促进宝宝的发育，并进行正确的引导和教育。', N'031089A')
GO
GO
SET IDENTITY_INSERT [questionnaire] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE [user]
GO
CREATE TABLE [user] (
[id] int NOT NULL IDENTITY(1,1) ,
[name] nvarchar(20) NULL ,
[pw] nvarchar(20) NULL ,
[type] int NULL 
)


GO
DBCC CHECKIDENT(N'[user]', RESEED, 6)
GO

-- ----------------------------
-- Records of user
-- ----------------------------
BEGIN TRANSACTION
GO
SET IDENTITY_INSERT [user] ON
GO
INSERT INTO [user] ([id], [name], [pw], [type]) VALUES (N'1', N'admin', N'123', N'1'), (N'6', N'a', N'123', N'1')
GO
GO
SET IDENTITY_INSERT [user] OFF
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Indexes structure for table answer
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table answer
-- ----------------------------
ALTER TABLE [answer] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table question
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table question
-- ----------------------------
ALTER TABLE [question] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table questionnaire
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table questionnaire
-- ----------------------------
ALTER TABLE [questionnaire] ADD PRIMARY KEY ([id])
GO

-- ----------------------------
-- Indexes structure for table user
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table user
-- ----------------------------
ALTER TABLE [user] ADD PRIMARY KEY ([id])
GO

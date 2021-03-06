数据库下载地址:: http://www.kexunsoftware.com/productDetail/5d26d7f8e7164beb962475de45bcdad3

# 科讯餐饮管理系统介绍文档

**背景:**
		随着国家经济的不断蓬勃发展，国民生活水平和消费水平的提高带动了餐饮行业的发展。在快速发展的同时，多数餐饮业在同常经营管理中仍普遍采用手工管理方式，餐饮管理信息系统是传统餐饮服务业与计算机信息管理相结合的成果，它能有效避免手工管理中的弊病，实现管理方式的升级。

​		随着餐饮店规模的不断扩大，手工管理在工作效率、人员成本、提供决策信息方面都已经难以适应现代化经营管理的要求,严重制约了整个餐饮业的规模化发展和整体服务水平的提升。国外很早就开始在餐饮业采用计算机进行信息管理，随着计算机的飞速发展，经过几十年的开发及应用，已经非常成熟。随着中国对外的不断开放，餐饮管理信息系统正在越来越多的被国内餐饮企业应用于餐饮管理领域。

​		餐饮业务涉及的各个工作环节已不再仅仅是传统的管理、业务结算，而是更广、更全的服务性行业代表。特别是近年来我国的餐饮业面临更加激烈的同业竞争，如何提供更多的工作流程和更优质的的服务，如何吸引更多的顾客，如何利用计算机技术加强顾客账户信息管理、进行顾客业务再造,提高员工的工作效率和业务竞争能力是摆在各餐饮经营者面前的一上迫切需要解决的问题。

​		餐饮业是一个服务性行业、从选餐、结算等整个过程应该能够体现以人为中心，提供快捷、方便的服务，给顾客感受一种顾客至上的享受，提高管理水平，简化各种复杂操作，在最合理最短时间内完成业务规范操作，这样才能令客舒适难忘，增加顾客回头率。本系统设计的主要意义在于它能够切实有效地指导工作人员规范业务操作流程，更高效、快捷地实现业务的管理，保障顾客信息的安全，提高管理水平和工作效率，进而提高业务竞争能力

**系统简介:**

科讯餐饮管理系统 使用C#+SqlServer数据库编写开发 将点餐,财务报表,预定等功能融为一体,使得餐饮行业更规范的管理

**系统功能介绍**

**登录**

系统支持多用户登录,一键退出,一键锁定功能 并且可自由修改账号密码

默认登录用户名:lidong  密码:123

![](images/%E7%99%BB%E5%BD%95-1623239592194.png)



**系统主页**

详细介绍请看如下图所示:

![1623241049139](images/1623241049139.png)



**开单消费:**

选择鼠标左键[可用]餐桌>点击菜单栏[开单]选项 弹出页面如下

![](images/3.png)

在顾客人数输入框可输入用餐人数 并且可自由选择是否开桌立即进行消费,也可以稍后在左边菜单栏目进行消费

点击确定会弹出点餐页面 如下图所示:

![](images/4.png)

在商品搜索框可输入商品名称模糊匹配商品,并且可以指定商品数量

也可以选择项目列表根据分类进行点餐,操作于清单操作相同

点单数量:会统计商品品类数量

合计金额:系统会自动计算订单总结一提供预览

退菜: 选择已点的菜品,点击退菜按钮即可退菜

点单关闭:关闭点单页面,点餐的菜品将自动保存

宾客结账:将在下面详细提到

正常操作[点单关闭] 会自动保存菜品回到主页,选择餐桌可预览所点菜品详细,点餐总价,并且可以一键结账单打印收据 如下图所示:

![1623240658744](images/1623240658744.png)



**消费:**

选择[使用中]餐桌 点击菜单栏消费按钮 弹出如下页面 可进行 点单,退菜 等操作 于上述开单即消费操作相同 如下图所示:

![](images/4-1623241504126.png)



**结账:**

结账是可输入会员编号 系统自动匹配会员信息对结账金额进行抵扣

可根据收用户的钱自动计算出找零

可选择打印账单 结账后会进行打印

![](images/6.png)



会员管理:

可对会员进行 新增  修改  删除  查询

![](images/8.png)

新增会员:

![](images/9.png)

营业报表:

折线图统计日营业报表

可选择日期查询 日收入  月收入  年收入

![](images/12.png)

辅助功能:

计算器 :调用系统计算器

 便签:类似于记事本,可保存一些笔记,点击保存

便签:

![](images/14.png)

预定管理

可进行预定  修改预定  删除预定   预定到达可直接生成餐桌订单



系统设置:

房间管理: 对房间,餐桌等信息进行维护

商品管理: 对商品种类进行维护  可新增  修改  删除  商品数据  也可根据名称模糊查找商品

系统设置:  对系统名称  欢迎词进行设置    对管理员进行维护   对会员等级进行维护每个等级享受有不同力度的折扣

如下图:

![](images/16.png)

餐桌数据维护

![17](images/17.png)

![18](images/18.png)

![19](images/19.png)

![20](images/20.png)

![21](images/21.png)

![22](images/22.png)

![23](images/23.png)

![24](images/24.png)

![25](images/25.png)

添加商品

![26](images/26.png)

![27](images/27.png)

添加管理员

![28](images/28.png)

修改管理员

![29](images/29.png)

新增会员等级

![30](images/30.png)

修改会员

![31](images/31.png)



完结
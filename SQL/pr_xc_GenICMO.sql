
/****** Object:  StoredProcedure [dbo].[pr_xc_GenICMO]    Script Date: 09/12/2019 22:41:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[pr_xc_GenICMO]
	-- Add the parameters for the stored procedure here
    @FICMOID AS INT ,
    @Fintedtify AS NVARCHAR(50)
AS 
SET XACT_ABORT on
begin tran
    BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
        SET NOCOUNT ON;

    -- Insert statements for procedure here
	
        SELECT  REFID = IDENTITY( INT , 1, 1 ),
                [FBillNo] ,
                [FWorkShop] ,
                [FItemNum] ,
                [FItemName] ,
                [Unit] ,
                [FQTY] ,
                [FFGQTY] ,
                [FBeginDate] ,
                [FEndDate] ,
                [FICMOID] ,
                [Fmodel] ,
                [Fcust] ,
                [FSaleNo] ,
                [FDelivDate] ,
                [FSaleEntryID] ,
                [Fintedtify]
        INTO    #t
        FROM    [dbo].[t_xc_ICMO]  with(updlock)
        WHERE   [Fintedtify] = @Fintedtify
  
        DECLARE @REFID INT
        SELECT  @REFID = MIN(REFID)
        FROM    #t
  
        DECLARE @p2 INT
        DECLARE @Fbillno AS NVARCHAR(50)
        DECLARE @FPlanCommitDate AS DATETIME
        DECLARE @FPlanFinishDate AS DATETIME
        DECLARE @FWorkShop AS INT
        DECLARE @FQty AS DECIMAL(28, 4)

  
  
  --    若最小行号不为空（有需要处理的数据）
        WHILE @REFID IS NOT NULL 
            BEGIN

    --    获取当前处理行的信息
                SELECT  @Fbillno = FBillNo ,
                        @FPlanCommitDate = FBeginDate ,
                        @FQty = FFGQTY ,
                        @FPlanFinishDate = [FEndDate],
                        @FWorkShop=FWorkShop
                        
                FROM    #t
                WHERE   REFID = @REFID

    /*     
    此处编写对当前行数据的业务逻辑处理代码        
    */
  
   
                SELECT  @p2 = MAX(finterid)
                FROM    icmo  with(updlock)
     
                EXEC GetICMaxNum 'ICMO', @p2 OUTPUT
                SELECT  @p2
     
                SELECT  *
                INTO    #tempIcmo
                FROM    icmo  with(updlock)
                WHERE   FInterId = @FICMOID

                UPDATE  #tempIcmo
                SET     FInterId = @p2 ,
                        FBillNo = @Fbillno ,
                        FPlanIssueDate = @FPlanCommitDate ,
                        FQty = 40 ,
                        FPlanCommitDate = @FPlanCommitDate ,
                        FPlanFinishDate = @FPlanFinishDate ,
                        FCheckDate = CONVERT(NVARCHAR,GETDATE(),111) ,
                        FTranType = 85 ,
                        FBrNo = '0' ,
                        FCancelLation = 0 ,
                        FAuxInHighLimitQty = ( 1 + FInHighLimit / FAuxQty )
                        * @FQty ,
                        FAuxInLowLimitQty = ( 1 - FInLowLimit / FAuxQty )
                        * @FQty ,
                        FAuxQty = @FQty ,
                        FGMPBatchNo = '' ,
                        FParentInterId = @FICMOID ,
                        FPlanCategory = ( SELECT    FPlanCategory
                                          FROM      ICMO
                                          WHERE     FInterID = @FICMOID
                                        ) ,
                        FBomCategory = ( SELECT FBomCategory
                                         FROM   ICMO
                                         WHERE  FInterID = @FICMOID
                                       ),
                         FWorkShop=@FWorkShop             

                INSERT  INTO Icmo
                        SELECT  *
                        FROM    #tempIcmo
 
 
                DROP TABLE #tempIcmo
  
  
                SELECT  @REFID = MIN(REFID)
                FROM    #t
                WHERE   REFID > @REFID

            END

        DROP TABLE #t

        UPDATE  Icmo
        SET     FCancelLation = 1 ,
                FPlanConfirmed = 0
        WHERE   FInterId = @FICMOID
        UPDATE  t1
        SET     t1.FBCommitQty = ( CASE WHEN ( t1.FBCommitQty - t2.FQty ) > 0
                                        THEN t1.FBCommitQty - t2.FQty
                                        ELSE 0
                                   END ) ,
                t1.FAuxBCommitQty = ( CASE WHEN ( t1.FAuxBCommitQty
                                                  - t2.FAuxQty ) > 0
                                           THEN t1.FAuxBCommitQty - t2.FAuxQty
                                           ELSE 0
                                      END )
        FROM    SEOrderEntry t1 ,
                ICMO t2
        WHERE   t1.FInterID = t2.FOrderInterID
                AND t1.FItemID = t2.FItemID
                AND t1.FEntryID = t2.FSourceEntryID
                AND t2.Forderinterid > 0
                AND t2.FInterID = @FICMOID
    END
    commit tran
SET XACT_ABORT off

GO



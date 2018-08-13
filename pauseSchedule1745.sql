 
 select * into Repair_Schedule_NeedPause from repair_Schedule where ScheduleType=1 and DATEDIFF(DD,OperaTime,GETDATE())=0 and FlagDel=0 and FlagNew=0
 update repair_Schedule set FlagNew=1 where ID in(select ID from Repair_Schedule_NeedPause)
 insert into repair_Schedule(AssignmentId,AssignmentProcedureId,ProcedureId,ScheduleType,ScheduleDate,Memo,FlagDel
 ,OperaDepId,OperaId,OperaName,OperaTime,AttachmentList_Schedule,PauseReason)
 select AssignmentId,AssignmentProcedureId,ProcedureId,2,convert(varchar(10),GETDATE(),120)+' 17:45:00','下班，系统自动暂停',FlagDel
 ,1,1,'系统管理员',getdate(),AttachmentList_Schedule,4 from Repair_Schedule_NeedPause
 drop table Repair_Schedule_NeedPause
 


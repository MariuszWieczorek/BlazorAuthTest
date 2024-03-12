select * from dbo.MWTech_route_IFS_idx02 as mwt
left join dbo.IfsRoutes as ifs
 ON  MWT.PART_NO = IFS.PartNo
            AND MWT.ROUTING_REVISION = IFS.RevisionNo
            AND MWT.ALTERNATIVE_NO = IFS.AlternativeNo 
            AND MWT.OPERATION_NO = IFS.OperationNo 
			AND (ISNUMERIC(ifs.AlternativeNo ) = 1 OR ifs.AlternativeNo = '*')
            AND (ISNUMERIC(ifs.RevisionNo ) = 1 OR ifs.RevisionNo = '*')
where mwt.PART_NO = 'DWY13614926NT' and mwt.ROUTING_REVISION = 6 and mwt.ALTERNATIVE_NO = '*'
AND IIF(ISNUMERIC(ifs.RevisionNo ) = 1,ifs.RevisionNo,0) >= '5'

select * from dbo.IfsRoutes as ifs
left join dbo.MWTech_route_IFS_idx02 as mwt
 ON  MWT.PART_NO = IFS.PartNo
            AND MWT.ROUTING_REVISION = IFS.RevisionNo
            AND MWT.ALTERNATIVE_NO = IFS.AlternativeNo 
            AND MWT.OPERATION_NO = IFS.OperationNo 
			AND (ISNUMERIC(ifs.AlternativeNo ) = 1 OR ifs.AlternativeNo = '*')
            AND (ISNUMERIC(ifs.RevisionNo ) = 1 OR ifs.RevisionNo = '*')
where ifs.PartNo = 'DWY13614926NT' and ifs.RevisionNo = 6 and ifs.AlternativeNo = '*'
AND IIF(ISNUMERIC(ifs.RevisionNo ) = 1,ifs.RevisionNo,0) >= '5'

select cpa.emp_no, cpa.person_id, cpa.fname, cpa.lname, cpa.emp_card from COMPANY_PERSON_ALL cpa
where 1 = 1 
--and emp_card is not null
-- and lower(lname) like '%fil%'
and emp_no = 'P03211'

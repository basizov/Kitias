import React, {useMemo, useState} from 'react';
import {
  Paper, styled,
  Table, TableBody,
  TableCell,
  TableContainer, TableFooter,
  TableHead, TablePagination,
  TableRow
} from "@mui/material";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {AttendenceType} from "../../model/Attendance/Attendence";
import {AttendanceCell} from "./AttendanceCell";

const StyledTableRow = styled(TableRow)(({theme}) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  '&:last-child td, &:last-child th': {
    border: 0,
  }
}));

const StyledTableHeadCell = styled(TableCell)(({theme}) => ({
  backgroundColor: theme.palette.action.hover
}));

const StyledTableHead = styled(TableHead)(({theme}) => ({
  backgroundColor: theme.palette.action.selected
}));

type PropsType = {
  subjectType: 'Лекция' | 'Практика';
};

export const AttendancesTable: React.FC<PropsType> = ({
                                                        subjectType
                                                      }) => {
  const pageSize = useMemo(() => 8, []);
  const [page, setPage] = useState(0);
  const {attendances} = useTypedSelector(s => s.attendance);
  const {subjects} = useTypedSelector(s => s.subject);
  const selectedSubject = useMemo(() => {
    return subjects.filter(s => s.type === subjectType);
  }, [subjects, subjectType])

  return (
    <TableContainer component={Paper}>
      <Table stickyHeader>
        <StyledTableHead>
          <TableRow>
            <StyledTableHeadCell
              align='center' rowSpan={2}
              sx={{borderRight: 1, borderRightColor: 'grey.800'}}
            >4443</StyledTableHeadCell>
            {selectedSubject.map((s, i) => (
              <React.Fragment key={`name ${s.id}`}>
                {s.type === subjectType &&
                page * pageSize < i + 1 && i + 1 < (page + 1) * pageSize &&
                <StyledTableHeadCell
                    align='center'
                    sx={{padding: 0}}
                >{`${s.type} ${i + 1}`}</StyledTableHeadCell>}
              </React.Fragment>))}
          </TableRow>
          <TableRow>
            {selectedSubject.map((s, i) => (
              <React.Fragment key={`date ${s.id}`}>
                {s.type === subjectType &&
                page * pageSize < i + 1 && i + 1 < (page + 1) * pageSize &&
                <StyledTableHeadCell
                    align='center'
                    sx={{padding: 0}}
                >{s.date}</StyledTableHeadCell>}
              </React.Fragment>))}
          </TableRow>
        </StyledTableHead>
        <TableBody>
          {Object.entries(attendances).map(([key, value]) => (
            <StyledTableRow key={key}>
              <TableCell
                sx={{width: '15rem', height: '2.35rem'}}
                aria-describedby={key}
              >{key}</TableCell>
              {[].map.call(value, (a: AttendenceType, i) => (
                <React.Fragment key={`data ${a.id}`}>
                  {a.type === subjectType &&
                  page * pageSize < i + 1 && i + 1 < (page + 1) * pageSize &&
                  <React.Fragment>
                      <AttendanceCell
                        identifier={a.id}
                        title={a.attended}
                      />
                  </React.Fragment>}
                </React.Fragment>))}
            </StyledTableRow>
          ))}
        </TableBody>
        {selectedSubject.length > 8 && <TableFooter>
            <TableRow>
                <TablePagination
                    count={selectedSubject.length}
                    rowsPerPage={pageSize}
                    page={page}
                    rowsPerPageOptions={[]}
                    onPageChange={(_, np) => setPage(np)}
                />
            </TableRow>
        </TableFooter>}
      </Table>
    </TableContainer>
  );
};

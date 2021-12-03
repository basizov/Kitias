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
import {
  AttendancesByStudents,
  AttendenceType
} from "../../model/Attendance/Attendence";
import {AttendanceCell} from "./AttendanceCell";
import {AttendanceCellScore} from "./AttendanceCellScore";

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
  subjectType: 'Лекция' | 'Практика' | 'Лабораторная работа';
  withScore?: boolean;
};

export const AttendancesTable: React.FC<PropsType> = ({
                                                        subjectType,
                                                        withScore = false
                                                      }) => {
  const pageSize = useMemo(() => 4, []);
  const [page, setPage] = useState(0);
  const {attendances, selectedSheduler} = useTypedSelector(s => s.attendance);
  const {subjects} = useTypedSelector(s => s.subject);
  const selectedSubject = useMemo(() => {
    return subjects.filter(s => s.type === subjectType);
  }, [subjects, subjectType]);
  const selectedAttendances = useMemo(() => {
    let result = {} as AttendancesByStudents;

    Object.entries(attendances).forEach(([key, value]) => {
      const filteredItems = [].filter.call(value, (a: AttendenceType) => a.type === subjectType);

      result[key] = filteredItems;
    });
    return result;
  }, [attendances, subjectType]);

  return (
    <TableContainer component={Paper}>
      <Table stickyHeader>
        <StyledTableHead>
          <TableRow>
            <StyledTableHeadCell
              align='center' rowSpan={2}
              sx={{borderRight: 1, borderRightColor: 'grey.800'}}
            >{selectedSheduler}</StyledTableHeadCell>
            {selectedSubject.map((s, i) => (
              <React.Fragment key={`name ${s.id}`}>
                {s.type === subjectType &&
                page * pageSize < i + 1 && i + 1 <= (page + 1) * pageSize &&
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
                page * pageSize < i + 1 && i + 1 <= (page + 1) * pageSize &&
                <StyledTableHeadCell
                    align='center'
                    sx={{padding: 0}}
                >{s.date}</StyledTableHeadCell>}
              </React.Fragment>))}
          </TableRow>
        </StyledTableHead>
        <TableBody>
          {Object.entries(selectedAttendances).map(([key, value]) => (
            <StyledTableRow key={key}>             <TableCell
                sx={{width: '15rem', height: '2.35rem'}}
                aria-describedby={key}
              >{key}</TableCell>
              {[].map.call(value, (a: AttendenceType, i) => (
                <React.Fragment key={`data ${a.id}`}>
                  {a.type === subjectType &&
                  page * pageSize < i + 1 && i + 1 <= (page + 1) * pageSize &&
                  <React.Fragment>
                    {!withScore ? <AttendanceCell
                      identifier={a.id}
                      title={a.attended}
                      defaultScore={a.score}
                    /> : <AttendanceCellScore
                      identifier={a.id}
                      title={a.score}
                      defaultAttended={a.attended}
                      ownKey={key}
                      base={a}
                    />}
                  </React.Fragment>}
                </React.Fragment>))}
            </StyledTableRow>
          ))}
        </TableBody>
        {selectedSubject.length > pageSize && <TableFooter>
            <TableRow>
                <TablePagination
                    count={selectedSubject.length}
                    rowsPerPage={pageSize}
                    page={page}
                    rowsPerPageOptions={[]}
                    onPageChange={(_, np) => setPage(np)}
                    labelDisplayedRows={({from, to, count}) => `${from}-${to} из ${count}`}
                />
            </TableRow>
        </TableFooter>}
      </Table>
    </TableContainer>
  );
};

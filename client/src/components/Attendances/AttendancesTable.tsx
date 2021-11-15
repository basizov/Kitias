import React, {useMemo, useState} from 'react';
import {
  Paper, Popover,
  styled,
  Table, TableBody,
  TableCell,
  TableContainer, TableFooter,
  TableHead, TablePagination,
  TableRow
} from "@mui/material";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {AttendenceType} from "../../model/Attendance/Attendence";

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

type SortedAttendanceType = {
  [key: string]: AttendenceType[];
};

type PropsType = {
  subjectType: 'Лекция' | 'Практика';
};

export const AttendancesTable: React.FC<PropsType> = ({
                                                        subjectType
                                                      }) => {
  const [anchorEl, setAnchorEl] = React.useState<HTMLTableCellElement | null>(null);
  const open = Boolean(anchorEl);
  const pageSize = useMemo(() => 8, []);
  const [page, setPage] = useState(0);
  const {attendances} = useTypedSelector(s => s.attendance);
  const specAttendance = useMemo(() => attendances
    .filter(a => a.type === subjectType), [attendances]);
  const studentAttendances = useMemo(() => {
    const result = specAttendance.reduce((fst, sec) => {
      fst[sec.fullName] = fst[sec.fullName] || [];
      fst[sec.fullName].push(sec);
      return fst;
    }, Object.create(null));

    return result as SortedAttendanceType[];
  }, [specAttendance]);

  return (
    <TableContainer component={Paper}>
      <Table stickyHeader>
        <StyledTableHead>
          <TableRow>
            <StyledTableHeadCell
              align='center' rowSpan={2}
              sx={{borderRight: 1, borderRightColor: 'grey.800'}}
            >4443</StyledTableHeadCell>
            {specAttendance.map((a, i) => (
              <React.Fragment key={`name ${a.id}`}>
                {a.type === subjectType &&
                page * pageSize < i + 1 && i + 1 < (page + 1) * pageSize &&
                <StyledTableHeadCell
                    align='center'
                    sx={{padding: 0}}
                >{`${a.type} ${i + 1}`}</StyledTableHeadCell>}
              </React.Fragment>))}
          </TableRow>
          <TableRow>
            {specAttendance.map((a, i) => (
              <React.Fragment key={`date ${a.id}`}>
                {a.type === subjectType &&
                page * pageSize < i + 1 && i + 1 < (page + 1) * pageSize &&
                <StyledTableHeadCell
                    align='center'
                    sx={{padding: 0}}
                >{a.date}</StyledTableHeadCell>}
              </React.Fragment>))}
          </TableRow>
        </StyledTableHead>
        <TableBody>
          {Object.keys(studentAttendances).map((prop: any) => (
            <StyledTableRow key={prop}>
              <TableCell
                aria-describedby={prop}
                onDoubleClick={(e) => setAnchorEl(e.currentTarget)}
              >{prop}</TableCell>
              <Popover
                id={prop}
                open={open}
                anchorEl={anchorEl}
                onClose={() => setAnchorEl(null)}
                anchorOrigin={{
                  vertical: 'bottom',
                  horizontal: 'left',
                }}
              >The content of the Popover.</Popover>
              {// @ts-ignore
                studentAttendances[prop].map((a: AttendenceType, i) => (
                  <React.Fragment key={`data ${a.id}`}>
                    {a.type === subjectType &&
                    page * pageSize < i + 1 && i + 1 < (page + 1) * pageSize &&
                    <React.Fragment>
                        <TableCell
                            aria-describedby={a.id}
                            align='center'
                            onDoubleClick={(e) => setAnchorEl(e.currentTarget)}
                        >{a.attended}</TableCell>
                    </React.Fragment>}
                  </React.Fragment>))}
            </StyledTableRow>
          ))}
        </TableBody>
        {attendances.length > 8 && <TableFooter>
            <TableRow>
                <TablePagination
                    count={specAttendance.length / Object.keys(studentAttendances).length}
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

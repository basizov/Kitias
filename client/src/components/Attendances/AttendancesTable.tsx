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
  const [anchorEl, setAnchorEl] = useState<HTMLTableCellElement | null>(null);
  const open = Boolean(anchorEl);
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
                aria-describedby={key}
                onDoubleClick={(e) => setAnchorEl(e.currentTarget)}
              >{key}</TableCell>
              <Popover
                id={key}
                open={open}
                anchorEl={anchorEl}
                onClose={() => setAnchorEl(null)}
                anchorOrigin={{
                  vertical: 'bottom',
                  horizontal: 'left',
                }}
              >The content of the Popover.</Popover>
              {[].map.call(value, (a: AttendenceType, i) => (
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

import React, {useEffect} from 'react';
import {
  Box,
  Paper, styled,
  Table, TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Typography
} from "@mui/material";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {useDispatch} from "react-redux";
import {getShedulerSAttendaces} from "../../store/attendanceStore/asyncActions";
import {Loading} from "../../layout/Loading";
import {useParams} from "react-router";
import {StyledTableHead, StyledTableHeadCell} from "./AttendancesTable";

const StyledBox = styled(Box)({
  display: 'flex',
  justifyContent: 'center',
  gap: '.3rem'
});

export const TotalResult: React.FC = () => {
  const dispatch = useDispatch();
  const params = useParams();
  const {
    loadingSA,
    selectedSheduler,
    sAttendances
  } = useTypedSelector(s => s.attendance);

  const selectColor = (attended: string) => {
    if (attended === 'Н') {
      return 'error';
    } else if (attended === 'Б') {
      return '#cfcf25';
    } else if (attended === 'О') {
      return 'secondary';
    }
    return '#2a8a21';
  };

  const selectBackColor = (score: number) => {
    if (score < 51) {
      return '#6b0a0a';
    } else if (score < 74) {
      return '#a34e4e';
    } else if (score < 81) {
      return '#cfcf25';
    }
    return '#2a8a21';
  };

  const selectGrade = (grade: string) => {
    if (grade === 'Отлично') {
      return '#2a8a21';
    } else if (grade === 'Хорошо') {
      return '#cfcf25';
    } else if (grade === 'Удовлетворительно') {
      return '#a34e4e';
    } else if (grade === '-') {
      return '';
    }
    return '#6b0a0a';
  };

  useEffect(() => {
    if (params.id) {
      dispatch(getShedulerSAttendaces(params.id));
    }
  }, []);

  if (loadingSA) {
    return <Loading loading={loadingSA}/>
  }
  return (
    <TableContainer component={Paper}>
      <Table stickyHeader>
        <StyledTableHead>
          <TableRow>
            <StyledTableHeadCell
              align='center'
              sx={{borderRight: 1, borderRightColor: 'grey.800'}}
            >{selectedSheduler}</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
            >Лекции</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
            >Практики</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
            >Лабороторные работы</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
            >Σ</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
            >Оценка</StyledTableHeadCell>
          </TableRow>
        </StyledTableHead>
        <TableBody>
          {sAttendances.map(s => (
            <TableRow key={s.id}>
              <TableCell>{s.studentName}</TableCell>
              <TableCell>
                <StyledBox>
                  {s.lectures.map(l => (
                    <Typography
                      key={l.id}
                      color={selectColor(l.attended)}
                    >{l.score}</Typography>
                  ))}
                </StyledBox>
              </TableCell>
              <TableCell>
                <StyledBox>
                  {s.practises.map(p => (
                    <Typography
                      key={p.id}
                      color={selectColor(p.attended)}
                    >{p.score}</Typography>
                  ))}
                </StyledBox>
              </TableCell>
              <TableCell>
                <StyledBox>
                  {s.laborotories.map(l => (
                    <Typography
                      key={l.id}
                      color={selectColor(l.attended)}
                    >{l.score}</Typography>
                  ))}
                </StyledBox>
              </TableCell>
              <TableCell
                align='center'
                sx={{
                  backgroundColor: selectBackColor(Number(s.raiting))
                }}
              >{s.raiting}</TableCell>
              <TableCell
                align='center'
              >{s.grade}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

import React, {useEffect, useState} from 'react';
import {
  Box,
  Paper, styled,
  Table, TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Typography, useMediaQuery
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
  flexWrap: 'wrap',
  maxWidth: '100%',
  margin: '0 auto',
  gap: '.3rem'
});

export const TotalResult: React.FC = () => {
  const dispatch = useDispatch();
  const params = useParams();
  const isTablet = useMediaQuery('(min-width: 760px)');
  const isMobile = useMediaQuery('(min-width: 560px)');
  const isGrade = useMediaQuery('(min-width: 360px)');
  const [selectedTab, setSelectedTab] = useState('Лекции');
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
  }, [dispatch, params.id]);

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
            >{isMobile ? selectedSheduler : ''}</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
              onClick={() => setSelectedTab('Лекции')}
            >{`${!isMobile ? 'Л' : 'Лекции'}`}</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
              onClick={() => setSelectedTab('Практики')}
            >{`${!isMobile ? 'П' : 'Практики'}`}</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
              onClick={() => setSelectedTab('Лабы')}
            >{`${!isMobile ? 'ЛР' : 'Лабороторные работы'}`}</StyledTableHeadCell>
            <StyledTableHeadCell
              align='center'
            >Σ</StyledTableHeadCell>
            {isGrade && <StyledTableHeadCell
              align='center'
            >{`${!isMobile ? 'О' : 'Оценка'}`}</StyledTableHeadCell>}
          </TableRow>
        </StyledTableHead>
        <TableBody>
          {sAttendances.map(s => (
            <TableRow key={s.id}>
              <TableCell>
                {isMobile ? s.studentName : s.studentName.split(' ')[0]}
              </TableCell>
              {(isTablet || selectedTab === 'Лекции') &&
              <TableCell colSpan={isTablet ? 1 : 3}>
                  <StyledBox>
                    {s.lectures.map(l => (
                      <Typography
                        key={l.id}
                        color={selectColor(l.attended)}
                      >{l.score}</Typography>
                    ))}
                  </StyledBox>
              </TableCell>}
              {(isTablet || selectedTab === 'Практики') &&
              <TableCell colSpan={isTablet ? 1 : 3}>
                  <StyledBox>
                    {s.practises.map(p => (
                      <Typography
                        key={p.id}
                        color={selectColor(p.attended)}
                      >{p.score}</Typography>
                    ))}
                  </StyledBox>
              </TableCell>}
              {(isTablet || selectedTab === 'Лабы') &&
              <TableCell colSpan={isTablet ? 1 : 3}>
                  <StyledBox>
                    {s.laborotories.map(l => (
                      <Typography
                        key={l.id}
                        color={selectColor(l.attended)}
                      >{l.score}</Typography>
                    ))}
                  </StyledBox>
              </TableCell>}
              <TableCell
                align='center'
                sx={{
                  backgroundColor: selectBackColor(Number(s.raiting))
                }}
              >{s.raiting}</TableCell>
              {isGrade && <TableCell
                align='center'
                sx={{
                  backgroundColor: selectGrade(s.grade)
                }}
              >{s.grade}</TableCell>}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

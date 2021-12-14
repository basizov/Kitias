import React, {useCallback, useEffect, useState} from 'react';
import {
  Box, FormControl, MenuItem,
  Paper, Select, styled,
  Table, TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Typography, useMediaQuery
} from "@mui/material";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {useDispatch} from "react-redux";
import {
  getGrades,
  getShedulerSAttendaces, updateSAttendance
} from "../../store/attendanceStore/asyncActions";
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

type PropsType = {
  id?: string;
  canChangeGrade?: boolean;
};

export const TotalResult: React.FC<PropsType> = ({
                                                   id = '',
                                                   canChangeGrade = true
                                                 }) => {
  const dispatch = useDispatch();
  const params = useParams();
  const isTablet = useMediaQuery('(min-width: 760px)');
  const isMobile = useMediaQuery('(min-width: 560px)');
  const isGrade = useMediaQuery('(min-width: 375px)');
  const [selectedTab, setSelectedTab] = useState('Лекции');
  const [showGrade, setShowGrade] = useState<boolean[]>([]);
  const [newGrades, setNewGrades] = useState<string[]>([]);
  const {
    loadingSA,
    selectedSheduler,
    sAttendances,
    grades
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

  const selectBackColor = useCallback((score: number) => {
    if (score < 51) {
      return '#6b0a0a';
    } else if (score < 74) {
      return '#a34e4e';
    } else if (score < 81) {
      return '#cfcf25';
    }
    return '#2a8a21';
  }, []);

  const selectGrade = useCallback((grade: string) => {
    if (grade === 'Отлично' || grade === '5') {
      return '#2a8a21';
    } else if (grade === 'Хорошо' || grade === '4') {
      return '#b8cf1e';
    } else if (grade === 'Удовлетворительно' || grade === '3') {
      return '#a34e4e';
    } else if (grade === 'Пересдача' || grade === '2') {
      return '#a32b39';
    } else if (grade === 'Не допущен' || grade === '1') {
      return '#a31515';
    } else if (grade === '-') {
      return '';
    }
    return '#6b0a0a';
  }, []);

  const convertToNumber = useCallback((grade: string) => {
    if (grade === 'Отлично') {
      return '5';
    } else if (grade === 'Хорошо') {
      return '4';
    } else if (grade === 'Удовлетворительно') {
      return '3';
    } else if (grade === 'Не допущен') {
      return '1';
    } else if (grade === '-') {
      return '-';
    }
    return '2';
  }, []);

  useEffect(() => {
    if (params.id) {
      dispatch(getGrades());
      dispatch(getShedulerSAttendaces(params.id));
    } else if (id) {
      dispatch(getGrades());
      dispatch(getShedulerSAttendaces(id));
    }
  }, [dispatch, params.id, id]);

  useEffect(() => {
    if (sAttendances) {
      setShowGrade(Array(sAttendances.length).fill(false));
      setNewGrades(sAttendances.map((sa) => convertToNumber(sa.grade)));
    }
  }, [sAttendances, convertToNumber]);

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
          {sAttendances.map((s, i) => (
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
                    cursor: 'pointer',
                    maxWidth: '5rem',
                    backgroundColor: selectGrade(newGrades[i])
                  }}
                  onClick={() => {
                    if (canChangeGrade) {
                      setShowGrade(
                        showGrade.map((sg, j) => j === i ? !sg : sg)
                      );
                    }
                  }}
              >{showGrade[i] ?
                <FormControl fullWidth>
                  <Select
                    id="grade"
                    size='small'
                    value={newGrades[i]}
                    onChange={async (e) => {
                      const grade = e.target.value;
                      let gradeEntity = e.target.value;

                      if (Number(grade) === 1) {
                        gradeEntity = 'Не допущен';
                      } else if (Number(grade) === 2) {
                        gradeEntity = 'Пересдача';
                      } else if (Number(grade) === 3) {
                        gradeEntity = 'Удовлетворительно';
                      } else if (Number(grade) === 4) {
                        gradeEntity = 'Хорошо';
                      } else if (Number(grade) === 5) {
                        gradeEntity = 'Отлично';
                      }
                      await dispatch(updateSAttendance(s.id, {
                        grade: gradeEntity,
                        raiting: s.raiting
                      }));
                      setNewGrades(
                        newGrades.map((ng, j) => i === j ? String(grade) : ng)
                      );
                    }}
                  >
                    {grades.map((g, k) => (
                      <MenuItem
                        key={g}
                        value={g === '-' ? '-' : k + 1}
                      >{g}</MenuItem>
                    ))}
                  </Select>
                </FormControl>
                : newGrades[i]}</TableCell>}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

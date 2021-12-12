import React, {useEffect, useMemo, useState} from 'react';
import {
  IconButton,
  Paper, styled,
  Table, TableBody,
  TableCell,
  TableContainer, TableFooter,
  TableHead, TablePagination,
  TableRow, useMediaQuery
} from "@mui/material";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {
  AttendancesByStudents,
  AttendenceType
} from "../../model/Attendance/Attendence";
import {AttendanceCell, StyledTableCell} from "./AttendanceCell";
import {AttendanceCellScore} from "./AttendanceCellScore";
import {ZoomIn, ZoomOut} from "@mui/icons-material";
import {ModalHoc} from "../HOC/ModalHoc";
import {UpdateSubject} from "../Subject/UpdateSubject";
import {SubjectType} from "../../model/Subject/Subject";
import {CreateSubject} from "../Subject/CreateSubject";
import {useDispatch} from "react-redux";
import {
  getAttendances, getAttendanceSubjects,
  getSheduler
} from "../../store/attendanceStore/asyncActions";
import {useParams} from "react-router";

const StyledTableRow = styled(TableRow)(({theme}) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  '&:last-child td, &:last-child th': {
    border: 0,
  }
}));

export const StyledTableHeadCell = styled(TableCell)(({theme}) => ({
  backgroundColor: theme.palette.action.hover
}));

export const StyledTableHead = styled(TableHead)(({theme}) => ({
  backgroundColor: theme.palette.action.selected
}));

export const StyledPaper = styled(Paper)(() => ({
  maxWidth: '100%'
}));

type PropsType = {
  subjectType: 'Лекция' | 'Практика' | 'Лабораторная работа';
  withScore?: boolean;
};

export const AttendancesTable: React.FC<PropsType> = ({
                                                        subjectType,
                                                        withScore = false
                                                      }) => {
  const dispatch = useDispatch();
  const params = useParams();
  const isTablet = useMediaQuery('(min-width: 950px)');
  const isMobile = useMediaQuery('(min-width: 530px)');
  const pageSize = useMemo(
    () => isTablet ? 4 : isMobile ? 2 : 1,
    [isMobile, isTablet]
  );
  const [page, setPage] = useState(0);
  const [showAll, setShowAll] = useState<boolean[]>([]);
  const {
    attendances,
    selectedSheduler
  } = useTypedSelector(s => s.attendance);
  const {subjects} = useTypedSelector(s => s.subject);
  const selectedSubjects = useMemo(() => {
    const typeSubjects = subjects
      .filter(s => s.type === subjectType)
      .map((s, i) => ({
        ...s,
        type: `${s.type} ${i + 1}`
      }));

    return typeSubjects.filter(s => s.isGiveScore || !withScore);
  }, [subjects, subjectType, withScore]);
  const [updateOpen, setUpdateOpen] = useState(false);
  const [openCreate, setOpenCreate] = useState(false);
  const [selectedSubject, setSelectedSubject] = useState<SubjectType | null>(null);
  const selectedAttendances = useMemo(() => {
    let result = {} as AttendancesByStudents;
    Object.entries(attendances).forEach(([key, value]) => {
      const filteredItems = [].filter.call(
        value,
        (a: AttendenceType, i) => !!selectedSubjects
          .find(s => s.date === a.date)
      );

      result[key] = filteredItems;
    });
    return result;
  }, [attendances, selectedSubjects]);
  const showProps = useMemo(
    () => Array.from(Array(
      (page + 1) * pageSize > selectedSubjects.length
        ? pageSize - ((page + 1) * pageSize - selectedSubjects.length) : pageSize
    ).keys()),
    [pageSize, page, selectedSubjects]
  );

  useEffect(() => {
    setShowAll(Array(pageSize).fill(false));
  }, [pageSize]);

  const closeCreateModel = async () => {
    if (params.id) {
      await dispatch(getAttendances(params.id));
      await dispatch(getSheduler(params.id));
      await dispatch(getAttendanceSubjects(params.id));
    }
    setOpenCreate(false);
  };

  return (
    <React.Fragment>
      <TableContainer component={StyledPaper}>
        <Table stickyHeader>
          <StyledTableHead
            sx={{cursor: 'pointer'}}
          >
            <TableRow>
              <StyledTableHeadCell
                align='center' rowSpan={2}
                sx={{
                  borderRight: 1,
                  borderRightColor: 'grey.800'
                }}
                onClick={() => setOpenCreate(true)}
              >{selectedSheduler}</StyledTableHeadCell>
              {selectedSubjects.map((s, i) => (
                <React.Fragment
                  key={`name ${s.id}`}
                >
                  {s.type.substring(0, s.type.lastIndexOf(' ')) === subjectType &&
                  page * pageSize < i + 1 && i + 1 <= (page + 1) * pageSize &&
                  <StyledTableHeadCell
                      align='center'
                      sx={{padding: 0}}
                      onClick={() => {
                        setSelectedSubject({
                          ...s,
                          type: s.type.substring(0, s.type.lastIndexOf(' '))
                        });
                        setUpdateOpen(true);
                      }}
                  >{`${s.type}`}</StyledTableHeadCell>}
                </React.Fragment>))}
            </TableRow>
            <TableRow>
              {selectedSubjects.map((s, i) => (
                <React.Fragment
                  key={`date ${s.id}`}
                >
                  {s.type.substring(0, s.type.lastIndexOf(' ')) &&
                  page * pageSize < i + 1 && i + 1 <= (page + 1) * pageSize &&
                  <StyledTableHeadCell
                      align='center'
                      sx={{padding: 0}}
                      onClick={() => {
                        setSelectedSubject({
                          ...s,
                          type: s.type.substring(0, s.type.lastIndexOf(' '))
                        });
                        setUpdateOpen(true);
                      }}
                  >{withScore ? s.theme || 'Нет темы' : s.date}</StyledTableHeadCell>}
                </React.Fragment>))}
            </TableRow>
          </StyledTableHead>
          <TableBody>
            {Object.entries(selectedAttendances).map(([key, value]) => (
              <StyledTableRow key={key}>
                <TableCell
                  sx={{maxWidth: '15rem', height: '2.35rem'}}
                  aria-describedby={key}
                >{key}</TableCell>
                {[].map.call(value, (a: AttendenceType, i) => {
                  if (a.type === subjectType &&
                    page * pageSize < i + 1 && i + 1 <= (page + 1) * pageSize) {
                    if (!withScore) {
                      return <AttendanceCell
                        key={`data ${a.id}`}
                        identifier={a.id}
                        title={a.attended}
                        defaultScore={a.score}
                        ownKey={key}
                        base={a}
                        showAll={showAll[i]}
                      />;
                    }
                    return <AttendanceCellScore
                      key={`data ${a.id}`}
                      identifier={a.id}
                      title={a.score}
                      defaultAttended={a.attended}
                      ownKey={key}
                      base={a}
                      showAll={showAll[i]}
                    />;
                  }
                })}
              </StyledTableRow>
            ))}
            <StyledTableRow>
              <TableCell
                sx={{
                  maxWidth: '15rem',
                  height: '2.35rem',
                  padding: 0,
                  paddingTop: '.3rem'
                }}
              ></TableCell>
              {showProps.map(key => (
                <StyledTableCell
                  key={key}
                  align='center'
                  sx={{padding: 0, paddingTop: '.3rem'}}
                >
                  <IconButton
                    color='error'
                    size='small'
                    onClick={() => setShowAll(showAll
                      .map((sa, i) => i === key ? false : sa))
                    }
                  ><ZoomOut/></IconButton>
                  <IconButton
                    color='success'
                    size='small'
                    onClick={() => setShowAll(showAll
                      .map((sa, i) => i === key ? true : sa))
                    }
                  ><ZoomIn/></IconButton>
                </StyledTableCell>
              ))}
            </StyledTableRow>
          </TableBody>
          {selectedSubjects.length > pageSize && <TableFooter>
              <TableRow>
                  <TablePagination
                      count={selectedSubjects.length}
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
      <ModalHoc
        open={updateOpen}
        onClose={() => setUpdateOpen(false)}
      ><UpdateSubject
        subject={selectedSubject!}
        close={() => setUpdateOpen(false)}
      /></ModalHoc>
      <ModalHoc
        open={openCreate}
        onClose={closeCreateModel}
      ><CreateSubject close={closeCreateModel}/></ModalHoc>
    </React.Fragment>
  );
};

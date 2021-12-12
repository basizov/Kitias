import React, {useEffect, useMemo, useState} from 'react';
import {Button, ButtonGroup, styled, TableCell} from "@mui/material";
import {useDispatch} from "react-redux";
import {updateAttendance} from "../../store/attendanceStore/asyncActions";
import {
  AttendenceType
} from "../../model/Attendance/Attendence";
import {attendanceActions} from "../../store/attendanceStore";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {format, parse} from "date-fns";

type PropsType = {
  identifier: string;
  title: string;
  defaultScore: string;
  ownKey: string;
  base: AttendenceType;
  showAll?: boolean;
};

export const StyledTableCell = styled(TableCell)({
  cursor: 'pointer',
  userSelect: 'none',
  maxWidth: '12rem',
  minWidth: '9rem'
});

export const AttendanceCell: React.FC<PropsType> = ({
                                                      identifier,
                                                      title,
                                                      defaultScore,
                                                      base,
                                                      ownKey,
                                                      showAll = false
                                                    }) => {
  const dispatch = useDispatch();
  const [showPop, setShowPop] = useState(false);
  const [changed, setChanged] = useState(false);
  const [attendace, setAttendace] = useState('');
  const {attendances} = useTypedSelector(s => s.attendance);
  const isNotValid = useMemo(
    () => parse(base.date, 'dd.MM.yyyy', new Date()) >
      parse(format(new Date(), 'dd.MM.yyyy'), 'dd.MM.yyyy', new Date()),
    [base]
  );

  const changeAttendace = async (attendace: string) => {
    const updatedAttendace = {
      ...base,
      attended: attendace,
      score: defaultScore
    } as AttendenceType;

    await dispatch(updateAttendance(identifier, updatedAttendace));
    setChanged(true);
    setAttendace(attendace);
    setShowPop(false);
    const newAttendances =
      Object.entries(attendances).map(([key, value]) =>
        key !== ownKey ?
          [key, value] :
          [key, [].map.call(value, (a: AttendenceType) => a.id === identifier ? updatedAttendace : a)]
      );

    dispatch(attendanceActions.setAttendances(
      Object.fromEntries(newAttendances)
    ));
  };

  useEffect(() => {
    setShowPop(showAll);
  }, [showAll]);

  return (
    <StyledTableCell
      align='center'
      onClick={() => {
        if (isNotValid) {
          return;
        }
        setShowPop((prev) => !prev);
      }}
    >
      {showPop ? <ButtonGroup>
        <Button
          color='error'
          onClick={(e) => {
            e.preventDefault();
            e.stopPropagation();
            changeAttendace('Н');
          }}
          size='small'
        >Н</Button>
        <Button
          color='secondary'
          onClick={(e) => {
            e.preventDefault();
            e.stopPropagation();
            changeAttendace('О');
          }}
          size='small'
        >О</Button>
        <Button
          color='warning'
          onClick={(e) => {
            e.preventDefault();
            e.stopPropagation();
            changeAttendace('Б');
          }}
          size='small'
        >Б</Button>
        <Button
          color='success'
          onClick={(e) => {
            e.preventDefault();
            e.stopPropagation();
            changeAttendace('+');
          }}
          size='small'
        >+</Button>
      </ButtonGroup> : changed ? attendace : title}
    </StyledTableCell>
  );
};

import React, {useState} from 'react';
import {Button, ButtonGroup, styled, TableCell} from "@mui/material";
import {useDispatch} from "react-redux";
import {updateAttendance} from "../../store/attendanceStore/asyncActions";
import {
  AttendancesByStudents,
  AttendenceType
} from "../../model/Attendance/Attendence";
import {attendanceActions} from "../../store/attendanceStore";
import {useTypedSelector} from "../../hooks/useTypedSelector";

type PropsType = {
  identifier: string;
  title: string;
  defaultScore: string;
  ownKey: string;
  base: AttendenceType;
};

export const StyledTableCell = styled(TableCell)({
  cursor: 'pointer',
  userSelect: 'none',
  width: '12rem'
});

export const AttendanceCell: React.FC<PropsType> = ({
                                                      identifier,
                                                      title,
                                                      defaultScore,
                                                      base,
                                                      ownKey
                                                    }) => {
  const dispatch = useDispatch();
  const [showPop, setShowPop] = useState(false);
  const [changed, setChanged] = useState(false);
  const [attendace, setAttendace] = useState('');
  const {attendances} = useTypedSelector(s => s.attendance);

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
    dispatch(attendanceActions.setAttendances(
      Object.entries(attendances).map(([key, value]) =>
        key === ownKey ?
          [].map.call(value, (a: AttendenceType) => a.id === identifier ? updatedAttendace : a)
          : value
      ) as AttendancesByStudents[]
    ));
  };

  return (
    <StyledTableCell
      align='center'
      onDoubleClick={() => setShowPop((prev) => !prev)}
    >
      {showPop ? <ButtonGroup>
        <Button
          color='error'
          onClick={() => changeAttendace('Н')}
        >Н</Button>
        <Button
          color='secondary'
          onClick={() => changeAttendace('О')}
        >О</Button>
        <Button
          color='warning'
          onClick={() => changeAttendace('Б')}
        >Б</Button>
        <Button
          color='success'
          onClick={() => changeAttendace('+')}
        >+</Button>
      </ButtonGroup> : changed ? attendace : title}
    </StyledTableCell>
  );
};

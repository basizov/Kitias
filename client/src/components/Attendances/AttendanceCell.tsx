import React, {useState} from 'react';
import {Button, ButtonGroup, styled, TableCell} from "@mui/material";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {useDispatch} from "react-redux";
import {updateAttendance} from "../../store/attendanceStore/asyncActions";

type PropsType = {
  identifier: string;
  title: string;
};

const StyledTableCell = styled(TableCell)({
  cursor: 'pointer',
  userSelect: 'none',
  width: '12rem'
});

export const AttendanceCell: React.FC<PropsType> = ({identifier, title}) => {
  const dispatch = useDispatch();
  const [showPop, setShowPop] = useState(false);
  const [changed, setChanged] = useState(false);
  const [attendace, setAttendace] = useState('');
  const {error} = useTypedSelector(s => s.attendance);

  const changeAttendace = async (attendace: string) => {
    await dispatch(updateAttendance(identifier, {
      attended: attendace,
      score: '0'
    }));
    if (!error) {
      setChanged(true);
      setAttendace(attendace);
      setShowPop(false);
    }
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
          color='warning'
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

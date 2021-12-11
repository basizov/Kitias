import React, {useState} from 'react';
import {
  Box,
  FormControl, IconButton, OutlinedInput,
} from "@mui/material";
import {useTypedSelector} from "../../hooks/useTypedSelector";
import {useDispatch} from "react-redux";
import {updateAttendance} from "../../store/attendanceStore/asyncActions";
import {Check} from "@mui/icons-material";
import {attendanceActions} from "../../store/attendanceStore";
import {
  AttendenceType
} from "../../model/Attendance/Attendence";
import {StyledTableCell} from "./AttendanceCell";

type PropsType = {
  identifier: string;
  title: string;
  defaultAttended: string;
  ownKey: string;
  base: AttendenceType;
};

export const AttendanceCellScore: React.FC<PropsType> = ({
                                                           identifier,
                                                           title,
                                                           defaultAttended,
                                                           ownKey,
                                                           base
                                                         }) => {
  const dispatch = useDispatch();
  const [showPop, setShowPop] = useState(false);
  const [changed, setChanged] = useState(false);
  const [score, setScore] = useState(Number(title));
  const {attendances} = useTypedSelector(s => s.attendance);

  return (
    <StyledTableCell
      align='center'
      onClick={() => setShowPop((prev) => !prev)}
    >
      {showPop ? <Box sx={{
        display: 'flex',
        gap: '.7rem'
      }}>
        <FormControl
          variant="outlined"
          fullWidth
          size='small'
        >
          <OutlinedInput
            id="score"
            type='number'
            value={Number(score)}
            onClick={(e) => {
              e.preventDefault();
              e.stopPropagation();
            }}
            onChange={(e) => {
              const nbr = Number(e.target.value);

              if (nbr <= 100 && nbr >= 0) {
                setScore(nbr);
              }
            }}
            onFocus={(e) => e.target.select()}
            inputProps={{min: 0, max: 100}}
          />
        </FormControl>
        <IconButton
          color='success'
          onClick={async () => {
            const updatedAttendace = {
              ...base,
              attended: defaultAttended,
              score: String(score)
            } as AttendenceType;

            await dispatch(updateAttendance(identifier, {
              attended: defaultAttended,
              score: String(score)
            }));

            setChanged(true);
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
          }}
        >
          <Check/>
        </IconButton>
      </Box> : changed ? score : title}
    </StyledTableCell>
  );
};

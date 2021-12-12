import React, {useEffect, useMemo, useState} from 'react';
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
import {format, parse} from "date-fns";

type PropsType = {
  identifier: string;
  title: string;
  defaultAttended: string;
  ownKey: string;
  base: AttendenceType;
  showAll?: boolean;
};

export const AttendanceCellScore: React.FC<PropsType> = ({
                                                           identifier,
                                                           title,
                                                           defaultAttended,
                                                           ownKey,
                                                           base,
                                                           showAll = false
                                                         }) => {
  const dispatch = useDispatch();
  const [showPop, setShowPop] = useState(false);
  const [changed, setChanged] = useState(false);
  const [score, setScore] = useState(Number(title));
  const {attendances} = useTypedSelector(s => s.attendance);
  const isNotValid = useMemo(
    () => parse(base.date, 'dd.MM.yyyy', new Date()) >
      parse(format(new Date(), 'dd.MM.yyyy'), 'dd.MM.yyyy', new Date()),
    [base]
  );

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

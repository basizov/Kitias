import React from 'react';
import {TableCell, TableRow} from "@mui/material";

export const AttendanceHeader: React.FC = () => {
  return (
    <React.Fragment>
      <TableRow>
        <TableCell rowSpan={2}/>
        <TableCell rowSpan={2}>ФИО</TableCell>
        <TableCell
          align='center'
          colSpan={5}
          sx={{border: 0, padding: 0}}
        >Заметки посещения</TableCell>
        <TableCell align='right' rowSpan={2}>Σ</TableCell>
        <TableCell align='right' rowSpan={2}>Оценка</TableCell>
      </TableRow>
      <TableRow>
        <TableCell
          align='center'
          sx={{padding: 0}}
        >Лек.</TableCell>
        <TableCell
          align='center'
          sx={{padding: 0}}
        >Пр.</TableCell>
        <TableCell
          align='center'
          sx={{padding: 0}}
        >Лб.</TableCell>
        <TableCell
          align='center'
          sx={{padding: 0}}
        >Кр.</TableCell>
        <TableCell
          align='center'
          sx={{padding: 0}}
        >Дз.</TableCell>
      </TableRow>
    </React.Fragment>
  );
};

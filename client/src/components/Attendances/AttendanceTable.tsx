import React from 'react';
import {
  Paper,
  Table, TableBody,
  TableContainer,
  TableHead,
} from "@mui/material";
import {AttendanceHeader} from "./AttendanceHeader";
import {AttendanceRow} from "./AttendanceRow";

export const AttendanceTable: React.FC = () => {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <AttendanceHeader/>
        </TableHead>
        <TableBody>
          <AttendanceRow/>
          <AttendanceRow/>
          <AttendanceRow/>
          <AttendanceRow/>
          <AttendanceRow/>
        </TableBody>
      </Table>
    </TableContainer>
  );
};

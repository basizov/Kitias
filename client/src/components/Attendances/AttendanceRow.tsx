import React, {useState} from 'react';
import {
  Collapse, FormControl,
  Grid,
  IconButton, InputLabel, MenuItem, Paper, Select, Table,
  TableBody,
  TableCell, TableContainer,
  TableHead,
  TableRow
} from "@mui/material";
import {KeyboardArrowDown, KeyboardArrowUp} from "@mui/icons-material";

export const AttendanceRow: React.FC = () => {
  const [open, setOpen] = useState(false);

  return (
    <React.Fragment>
      <TableRow>
        <TableCell>
          <IconButton
            size="small"
            onClick={() => setOpen(!open)}
          >{open ? <KeyboardArrowUp/> : <KeyboardArrowDown/>}</IconButton>
        </TableCell>
        <TableCell>Сизов Борис Александрович</TableCell>
        <TableCell align='center'>
          <Grid container spacing={1} justifyContent='center'>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
          </Grid>
        </TableCell>
        <TableCell align='center'>
          <Grid container spacing={1} justifyContent='center'>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>uн</Grid>
            <Grid item>нu</Grid>
            <Grid item>uн</Grid>
          </Grid>
        </TableCell>
        <TableCell align='center'>
          <Grid container spacing={1} justifyContent='center'>
            <Grid item>yн</Grid>
            <Grid item>yн</Grid>
            <Grid item>нy</Grid>
            <Grid item>нy</Grid>
            <Grid item>yн</Grid>
          </Grid>
        </TableCell>
        <TableCell>-</TableCell>
        <TableCell>-</TableCell>
        <TableCell align='right'>50</TableCell>
        <TableCell align='right'>5</TableCell>
      </TableRow>
      <TableRow>
        <TableCell sx={{paddingTop: 0, paddingBottom: 0}} colSpan={9}>
          <Collapse in={open} unmountOnExit>
            <Grid
              container
              spacing={1}
              direction='column'
              sx={{padding: '.3rem'}}
            >
              <Grid item sx={{marginTop: '.7rem'}}>
                <FormControl sx={{width: '15rem'}}>
                  <InputLabel id="theme-type-label">Темы предмета</InputLabel>
                  <Select
                    id="theme-type"
                    labelId="theme-type-label"
                    label='Темы предмета'
                  >
                    <MenuItem value={''}>Любые</MenuItem>
                  </Select>
                </FormControl>
                <FormControl sx={{width: '15rem', marginLeft: '.7rem'}}>
                  <InputLabel id="subject-type-label">Типы предмета</InputLabel>
                  <Select
                    id="subject-type"
                    labelId="subject-type-label"
                    label='Типы предмета'
                  >
                    <MenuItem value={''}>Любые</MenuItem>
                  </Select>
                </FormControl>
              </Grid>
              <Grid item>
                <TableContainer component={Paper}>
                  <Table>
                    <caption
                      style={{textAlign: 'right'}}
                    >Количество баллов: <span
                      style={{fontWeight: 'bold', fontSize: 16}}
                    >{12}</span>
                    </caption>
                    <TableHead>
                      <TableRow>
                        <TableCell align='center'>Тема пары</TableCell>
                        <TableCell align='right'>Тип предмета</TableCell>
                        <TableCell align='right'>Кол-во баллов</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>

                    </TableBody>
                  </Table>
                </TableContainer>
              </Grid>
            </Grid>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
};

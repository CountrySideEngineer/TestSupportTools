#include <stdio.h>
#include <windows.h>
#include "min_unit.h"
#include "UserHeader.h"
//No global variables are refered by function sample_function_001.

//Test target function declare.
int sample_function_001(int input1, int input2);

//Initialize test stub buffers.
void sample_function_001_utest_SetUp()
{
}


static char* sample_function_001_utest_1()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(0 == _ret_val);
}

static char* sample_function_001_utest_2()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(1 == _ret_val);
}

static char* sample_function_001_utest_3()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(2 == _ret_val);
}

static char* sample_function_001_utest_4()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(3 == _ret_val);
}

static char* sample_function_001_utest_5()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(1 == _ret_val);
}

static char* sample_function_001_utest_6()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 1;
	input2 = 1;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(0 == _ret_val);
}

static char* sample_function_001_utest_7()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 1;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(1 == _ret_val);
}

static char* sample_function_001_utest_8()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 1;
	input2 = 3;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(2 == _ret_val);
}

static char* sample_function_001_utest_9()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(2 == _ret_val);
}

static char* sample_function_001_utest_10()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 2;
	input2 = 1;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(1 == _ret_val);
}

static char* sample_function_001_utest_11()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 2;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(0 == _ret_val);
}

static char* sample_function_001_utest_12()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 2;
	input2 = 3;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(1 == _ret_val);
}

static char* sample_function_001_utest_13()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(3 == _ret_val);
}

static char* sample_function_001_utest_14()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 3;
	input2 = 1;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(2 == _ret_val);
}

static char* sample_function_001_utest_15()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 3;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(1 == _ret_val);
}

static char* sample_function_001_utest_16()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 3;
	input2 = 3;

	//Initialize stub parameters.
	sample_function_001_utest_SetUp();

	int _ret_val = sample_function_001(input1, input2);

	mu_assert(0 == _ret_val);
}

char* sample_function_001_utest_run_all()
{
	mu_run_test("sample_function_001_utest_1", sample_function_001_utest_1);
	mu_run_test("sample_function_001_utest_2", sample_function_001_utest_2);
	mu_run_test("sample_function_001_utest_3", sample_function_001_utest_3);
	mu_run_test("sample_function_001_utest_4", sample_function_001_utest_4);
	mu_run_test("sample_function_001_utest_5", sample_function_001_utest_5);
	mu_run_test("sample_function_001_utest_6", sample_function_001_utest_6);
	mu_run_test("sample_function_001_utest_7", sample_function_001_utest_7);
	mu_run_test("sample_function_001_utest_8", sample_function_001_utest_8);
	mu_run_test("sample_function_001_utest_9", sample_function_001_utest_9);
	mu_run_test("sample_function_001_utest_10", sample_function_001_utest_10);
	mu_run_test("sample_function_001_utest_11", sample_function_001_utest_11);
	mu_run_test("sample_function_001_utest_12", sample_function_001_utest_12);
	mu_run_test("sample_function_001_utest_13", sample_function_001_utest_13);
	mu_run_test("sample_function_001_utest_14", sample_function_001_utest_14);
	mu_run_test("sample_function_001_utest_15", sample_function_001_utest_15);
	mu_run_test("sample_function_001_utest_16", sample_function_001_utest_16);

	return 0;
}


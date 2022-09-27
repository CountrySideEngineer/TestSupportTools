#include <stdio.h>
#include <windows.h>
#include "min_unit.h"
#include "UserHeader.h"
#include "sample_function_002_stub.h"
//No global variables are refered by function sample_function_002.

//Test target function declare.
int sample_function_002(int input1, int input2);

//Initialize test stub buffers.
void sample_function_002_utest_SetUp()
{
	subFuncA_002_init();
}


static char* sample_function_002_utest_1()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;
	subFuncA_002_return_value[0] = 1;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(1 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(0 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_2()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;
	subFuncA_002_return_value[0] = 2;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(2 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(1 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_3()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;
	subFuncA_002_return_value[0] = 4;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(4 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(2 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_4()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;
	subFuncA_002_return_value[0] = 8;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(8 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(3 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_5()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;
	subFuncA_002_return_value[0] = 1;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(1 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(1 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_6()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 1;
	input2 = 1;
	subFuncA_002_return_value[0] = 2;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(2 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(1 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_7()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 1;
	input2 = 2;
	subFuncA_002_return_value[0] = 4;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(4 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(2 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_8()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 1;
	input2 = 3;
	subFuncA_002_return_value[0] = 8;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(8 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(3 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_9()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;
	subFuncA_002_return_value[0] = 1;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(1 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(2 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_10()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 2;
	input2 = 1;
	subFuncA_002_return_value[0] = 2;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(2 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(2 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_11()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 2;
	input2 = 2;
	subFuncA_002_return_value[0] = 4;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(4 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(2 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_12()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 2;
	input2 = 3;
	subFuncA_002_return_value[0] = 8;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(8 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(3 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_13()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;
	subFuncA_002_return_value[0] = 1;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(1 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(3 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_14()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 3;
	input2 = 1;
	subFuncA_002_return_value[0] = 2;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(2 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(3 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_15()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 3;
	input2 = 2;
	subFuncA_002_return_value[0] = 4;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(4 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(3 == subFuncA_002_subInput1[0]);
}

static char* sample_function_002_utest_16()
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 3;
	input2 = 3;
	subFuncA_002_return_value[0] = 8;

	//Initialize stub parameters.
	sample_function_002_utest_SetUp();

	int _ret_val = sample_function_002(input1, input2);

	mu_assert(8 == _ret_val);
	mu_assert(1 == subFuncA_002_called_count);
	mu_assert(3 == subFuncA_002_subInput1[0]);
}

char* sample_function_002_utest_run_all()
{
	mu_run_test("sample_function_002_utest_1", sample_function_002_utest_1);
	mu_run_test("sample_function_002_utest_2", sample_function_002_utest_2);
	mu_run_test("sample_function_002_utest_3", sample_function_002_utest_3);
	mu_run_test("sample_function_002_utest_4", sample_function_002_utest_4);
	mu_run_test("sample_function_002_utest_5", sample_function_002_utest_5);
	mu_run_test("sample_function_002_utest_6", sample_function_002_utest_6);
	mu_run_test("sample_function_002_utest_7", sample_function_002_utest_7);
	mu_run_test("sample_function_002_utest_8", sample_function_002_utest_8);
	mu_run_test("sample_function_002_utest_9", sample_function_002_utest_9);
	mu_run_test("sample_function_002_utest_10", sample_function_002_utest_10);
	mu_run_test("sample_function_002_utest_11", sample_function_002_utest_11);
	mu_run_test("sample_function_002_utest_12", sample_function_002_utest_12);
	mu_run_test("sample_function_002_utest_13", sample_function_002_utest_13);
	mu_run_test("sample_function_002_utest_14", sample_function_002_utest_14);
	mu_run_test("sample_function_002_utest_15", sample_function_002_utest_15);
	mu_run_test("sample_function_002_utest_16", sample_function_002_utest_16);

	return 0;
}


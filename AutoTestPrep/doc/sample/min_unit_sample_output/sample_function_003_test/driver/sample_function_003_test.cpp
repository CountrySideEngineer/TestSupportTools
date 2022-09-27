#include <stdio.h>
#include <windows.h>
#include "min_unit.h"
#include "UserHeader.h"
//No global variables are refered by function sample_function_003.

//Test target function declare.
int sample_function_003(int input1, int* input2, SHORT input3);

//Initialize test stub buffers.
void sample_function_003_utest_SetUp()
{
}


static char* sample_function_003_utest_1()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;
	input3 = 1;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(1 == _ret_val);
	mu_assert(10 == input2);
}

static char* sample_function_003_utest_2()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;
	input3 = 2;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(1 == _ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_3()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;
	input3 = 3;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(1 == _ret_val);
	mu_assert(30 == input2);
}

static char* sample_function_003_utest_4()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;
	input3 = 1;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(2 == _ret_val);
	mu_assert(0 == input2);
}

static char* sample_function_003_utest_5()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;
	input3 = 2;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(1 == _ret_val);
	mu_assert(10 == input2);
}

static char* sample_function_003_utest_6()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;
	input3 = 3;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(1 == _ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_7()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;
	input3 = 1;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(2 == _ret_val);
	mu_assert(10 == input2);
}

static char* sample_function_003_utest_8()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;
	input3 = 2;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(2 == _ret_val);
	mu_assert(0 == input2);
}

static char* sample_function_003_utest_9()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;
	input3 = 3;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(1 == _ret_val);
	mu_assert(10 == input2);
}

static char* sample_function_003_utest_10()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;
	input3 = 1;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(2 == _ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_11()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;
	input3 = 2;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(2 == _ret_val);
	mu_assert(10 == input2);
}

static char* sample_function_003_utest_12()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;
	input3 = 3;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int _ret_val = sample_function_003(input1, &input2, input3);

	mu_assert(2 == _ret_val);
	mu_assert(0 == input2);
}

char* sample_function_003_utest_run_all()
{
	mu_run_test("sample_function_003_utest_1", sample_function_003_utest_1);
	mu_run_test("sample_function_003_utest_2", sample_function_003_utest_2);
	mu_run_test("sample_function_003_utest_3", sample_function_003_utest_3);
	mu_run_test("sample_function_003_utest_4", sample_function_003_utest_4);
	mu_run_test("sample_function_003_utest_5", sample_function_003_utest_5);
	mu_run_test("sample_function_003_utest_6", sample_function_003_utest_6);
	mu_run_test("sample_function_003_utest_7", sample_function_003_utest_7);
	mu_run_test("sample_function_003_utest_8", sample_function_003_utest_8);
	mu_run_test("sample_function_003_utest_9", sample_function_003_utest_9);
	mu_run_test("sample_function_003_utest_10", sample_function_003_utest_10);
	mu_run_test("sample_function_003_utest_11", sample_function_003_utest_11);
	mu_run_test("sample_function_003_utest_12", sample_function_003_utest_12);

	return 0;
}


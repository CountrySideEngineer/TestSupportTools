#include <stdio.h>
#include <windows.h>
#include "UserHeader.h"
#include "min_unit.h"

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
	input2 = 1;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(1 == ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_2()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(2 == ret_val);
}

static char* sample_function_003_utest_3()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(3 == ret_val);
	mu_assert(40 == input2);
}

static char* sample_function_003_utest_4()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(0 == ret_val);
	mu_assert(10 == input2);
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

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(1 == ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_6()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(2 == ret_val);
}

static char* sample_function_003_utest_7()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(3 == ret_val);
	mu_assert(40 == input2);
}

static char* sample_function_003_utest_8()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(1 == ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_9()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(2 == ret_val);
}

static char* sample_function_003_utest_10()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(3 == ret_val);
	mu_assert(40 == input2);
}

static char* sample_function_003_utest_11()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(0 == ret_val);
	mu_assert(10 == input2);
}

static char* sample_function_003_utest_12()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(1 == ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_13()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(2 == ret_val);
}

static char* sample_function_003_utest_14()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(3 == ret_val);
	mu_assert(40 == input2);
}

static char* sample_function_003_utest_15()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(1 == ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_16()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(2 == ret_val);
}

static char* sample_function_003_utest_17()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(3 == ret_val);
	mu_assert(40 == input2);
}

static char* sample_function_003_utest_18()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(0 == ret_val);
	mu_assert(10 == input2);
}

static char* sample_function_003_utest_19()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(1 == ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_20()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(2 == ret_val);
	mu_assert(30 == input2);
}

static char* sample_function_003_utest_21()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(3 == ret_val);
}

static char* sample_function_003_utest_22()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(1 == ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_23()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(2 == ret_val);
	mu_assert(30 == input2);
}

static char* sample_function_003_utest_24()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(3 == ret_val);
}

static char* sample_function_003_utest_25()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(0 == ret_val);
	mu_assert(10 == input2);
}

static char* sample_function_003_utest_26()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(1 == ret_val);
	mu_assert(20 == input2);
}

static char* sample_function_003_utest_27()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(2 == ret_val);
	mu_assert(30 == input2);
}

static char* sample_function_003_utest_28()
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	//Initialize stub parameters.
	sample_function_003_utest_SetUp();

	int returnValue = sample_function_003(input1, &input2, input3);

	mu_assert(3 == ret_val);
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
	mu_run_test("sample_function_003_utest_13", sample_function_003_utest_13);
	mu_run_test("sample_function_003_utest_14", sample_function_003_utest_14);
	mu_run_test("sample_function_003_utest_15", sample_function_003_utest_15);
	mu_run_test("sample_function_003_utest_16", sample_function_003_utest_16);
	mu_run_test("sample_function_003_utest_17", sample_function_003_utest_17);
	mu_run_test("sample_function_003_utest_18", sample_function_003_utest_18);
	mu_run_test("sample_function_003_utest_19", sample_function_003_utest_19);
	mu_run_test("sample_function_003_utest_20", sample_function_003_utest_20);
	mu_run_test("sample_function_003_utest_21", sample_function_003_utest_21);
	mu_run_test("sample_function_003_utest_22", sample_function_003_utest_22);
	mu_run_test("sample_function_003_utest_23", sample_function_003_utest_23);
	mu_run_test("sample_function_003_utest_24", sample_function_003_utest_24);
	mu_run_test("sample_function_003_utest_25", sample_function_003_utest_25);
	mu_run_test("sample_function_003_utest_26", sample_function_003_utest_26);
	mu_run_test("sample_function_003_utest_27", sample_function_003_utest_27);
	mu_run_test("sample_function_003_utest_28", sample_function_003_utest_28);

	return 0;
}


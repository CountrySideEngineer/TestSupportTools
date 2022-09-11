#include <stdio.h>
#include <windows.h>
#include "gtest/gtest.h"
#include "UserHeader.h"
#include "sample_function_002_stub.h"
//No global variables are refered by function sample_function_002.

//Test target function declare
int sample_function_002(int input1, int input2);

void sample_function_002_utest::SetUp()
{
	subFuncA_002_init();
}


TEST_F(sample_function_002_utest, sample_function_002_utest_1)
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	int _ret_val = sample_function_002(input1, input2);

	ASSERT_EQ(1, _ret_val);
}

TEST_F(sample_function_002_utest, sample_function_002_utest_2)
{
	//Declare argument for target
	int input1;
	int input2;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	int _ret_val = sample_function_002(input1, input2);

	ASSERT_EQ(2, _ret_val);
}


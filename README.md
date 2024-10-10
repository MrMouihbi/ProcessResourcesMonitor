# ProcessRessourcesMonitor

[![NuGet version](https://badge.fury.io/nu/ProcessResourcesMonitor.svg)](https://badge.fury.io/nu/ProcessResourcesMonitor)

This package helps track resources used by a specific process over specific intervals of time and store the collected metrics in many formats.<br/>
There are some good tools for this purpose (such as System.Diagnostics) but they only work perfectly on Windows, this package aims to support any OS (Windows, Linux, MacOs).

## Table of Contents

- [Installation](#installation)
- [Features](#features)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Installation

To install the ProcessResourcesMonitor package, run the following command in your terminal:

```bash
dotnet add package ProcessResourcesMonitor --version 1.0.1
```

## Features

- **Real-time Process Metrics Monitoring**: Collect real-time metrics such as CPU usage, memory consumption, thread count, and private bytes for any running process.
- **Flexible Data Storage Options**: Save collected metrics in multiple formats such as JSON, CSV, or in-memory storage to suit your application needs.

- **Customizable Collection Intervals**: Choose the desired interval for collecting metrics, allowing for fine-tuned monitoring based on your performance requirements.

- **Easy-to-Use API**: With simple-to-use methods, you can set up metric collection and storage with minimal code.

- **Status Tracking**: Monitor the status of the process, ensuring you know whether it's running or has exited.

- **Pluggable Storage**: Extendable storage interface lets you implement custom storage options (e.g., save data to databases or cloud storage).

- **Cross-Platform Compatibility**: Built to work on Windows, Mac, and Linux, making it a versatile solution for monitoring processes in any environment.

## Usage

To use the ProcessResourcesMonitor package, you can collect the metrics of any process by providing its process ID. This example demonstrates how to collect CPU and memory usage for a process with ID 652, store the metrics in memory, and save them as a JSON file.

```csharp
using ProcessResourcesMonitor;
using ProcessResourcesMonitor.Storage;

class Program {
    public static async Task Main(string[] args) {
        // Create a MetricsCollector instance for the process with ID 652
        var collector = new MetricsCollector(652);

        // Use memory storage to temporarily store collected metrics
        var storage = new MetricsMemoryStorage();

        // Start collecting metrics every 500 milliseconds
        await collector.CollectMetricsAsync(TimeSpan.FromMilliseconds(500), metrics => {
            // Store the collected metrics in memory
            storage.store(metrics);

            // Display the metrics stored in memory
            Console.WriteLine(storage.getMetrics().ToString());

            // Save the collected metrics as a JSON file
            storage.saveAsJson("metrics.json");

            // Display CPU usage and memory usage for the process
            Console.WriteLine($"CPU usage and memory usage: {metrics.CpuUsage}% CPU, {metrics.MemoryUsage} MB Memory");
        });
    }
}

```

Your `.json` file where you want to store the metrics (`metrics.json` in the last example) will be updated in real time and will look like :

```json
[
  {
    "Timestamp": 1728548899617,
    "CpuUsage": 0,
    "MemoryUsage": 17.609375,
    "ThreadCount": 6,
    "PrivateBytes": 0,
    "Status": "Running"
  },
  {
    "Timestamp": 1728548900158,
    "CpuUsage": 0,
    "MemoryUsage": 17.609375,
    "ThreadCount": 6,
    "PrivateBytes": 0,
    "Status": "Running"
  }
  // More metrics
]
```

#### CSV storage:

You can also store the metrics in a `.csv` file :

```csharp
var collector = new MetricsCollector(652);

        // Use CSV storage to store collected metrics in 'metrics.csv'
        var storage = new MetricsCsvStorage("metrics.csv");

        // Start collecting metrics every 500 milliseconds
        await collector.CollectMetricsAsync(TimeSpan.FromMilliseconds(500), metrics => {
            // Store the collected metrics in the CSV file
            storage.store(metrics);
        });
```

and your `.csv` file will be updated in real time and will look like this :

```csv
Timestamp,CpuUsage,MemoryUsage(MB),ThreadCount,PrivateBytes,Status
1728550660181,0,16.34375,6,0,Running
1728550660685,0,16.34375,6,0,Running
1728550661186,0.0011394735582293427,16.34375,6,0,Running
```

#### Description of Fields:

- Timestamp: The timestamp of when the metrics were collected.
- CpuUsage (MB): The percentage of CPU used by the process.
  MemoryUsage: The amount of memory (in MB) the process is using.
- ThreadCount: The number of active threads in the process.
- PrivateBytes (MB)s: The amount of private memory the process is consuming.
- Status: The current status of the process (e.g., "Running").

## Contributing

Contributions are welcome! If you have ideas or want to contribute improvements, please follow these steps:

1 - Fork the repository.

2 - Create a new branch for your feature or fix:<br>
`git checkout -b feature/your-feature` or `git checkout -b fix/your-fix`

3 - Commit your changes: <br>
`git commit -m "Add your message"`

4 - Push to the branch:<br>
`git push origin your-branch`.

5 - Create a pull request describing the changes you’ve made.
Make sure to check the existing issues and feature requests before starting work on something new.

## License

This project is licensed under the Apache License, Version 2.0. You may obtain a copy of the License at [http://www.apache.org/licenses/LICENSE-2.0](http://www.apache.org/licenses/LICENSE-2.0).
